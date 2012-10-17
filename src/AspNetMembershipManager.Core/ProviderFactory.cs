using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AspNetMembershipManager
{
	public class ProviderFactory : IProviderFactory
	{
		public TProvider CreateProviderFromConfig<TProvider>(ProviderSettings providerSettings) where TProvider : ProviderBase
		{
			Type providerType = GetProviderTypeFromLoadedAssemblies(providerSettings) ??
			                    GetProviderTypeFromExternalAssemblies(providerSettings);

            if (providerType == null)
            {
                throw new TypeLoadException(string.Format("Unable to load provider of type '{0}'", providerSettings.Type));
            }

			if (!typeof(TProvider).IsAssignableFrom(providerType))
			{
                throw new TypeLoadException(string.Format("Invalid provider type '{0}', it is not assignable to '{1}", providerType, typeof(TProvider)));
			}

			var provider = (TProvider)Activator.CreateInstance(providerType);
			provider.Initialize(providerSettings.Name, new NameValueCollection(providerSettings.Parameters));

			return provider;
		}

		private static Type GetProviderTypeFromLoadedAssemblies(ProviderSettings providerSettings)
		{
            return Type.GetType(providerSettings.Type);
            //foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            //{
            //    providerType = assembly.GetType(providerSettings.Type);

            //    if (providerType != null) break;
            //}
            //return providerType;
		}

	    private static Type GetProviderTypeFromExternalAssemblies(ProviderSettings providerSettings)
		{
			var webConfigDirectory = new FileInfo(providerSettings.CurrentConfiguration.FilePath).Directory;
	        var binDirectory = webConfigDirectory.GetDirectories("bin").FirstOrDefault();

            if (binDirectory == null)
            {
                throw new DirectoryNotFoundException("Could not find web site bin folder. Bin folder should be in the same directory as the web.config");
            }

			var typeName = new TypeName(providerSettings.Type);

	        return
	            AttemptToGetTypeFromSpecificAssemblyInBin(typeName, binDirectory) ??
	            AttemptToGetTypeFromAnyAssemblyInBin(typeName, binDirectory) ??
	            AttemptToGetTypeFromGacAssembly(typeName);
		}

        private static Type AttemptToGetTypeFromSpecificAssemblyInBin(TypeName typeName, DirectoryInfo binDirectory)
        {
            if (typeName.AssemblyName != null)
            {
                var assemblyFile = binDirectory.GetFiles(string.Format("{0}.dll", typeName.AssemblyName.Name)).SingleOrDefault();

                if (assemblyFile != null)
                {
                    Assembly assembly = Assembly.LoadFile(assemblyFile.FullName);

                    return AttemptToGetTypeFromAssembly(typeName, assembly);
                }
            }
            return null;
        }

	    private static Type AttemptToGetTypeFromAnyAssemblyInBin(TypeName typeName, DirectoryInfo binDirectory)
        {
            if (typeName.AssemblyName == null)
            {
                var binAssemblyFiles = binDirectory.GetFiles("*.dll");

                return binAssemblyFiles
                    .Select(binAssemblyFile => Assembly.LoadFile(binAssemblyFile.FullName))
                    .Select(assembly => AttemptToGetTypeFromAssembly(typeName, assembly))
                    .FirstOrDefault(providerType => providerType != null);
            }
	        return null;
        }

	    private static Type AttemptToGetTypeFromGacAssembly(TypeName typeName)
	    {
	        Assembly assembly = Assembly.Load(typeName.AssemblyName);

	        return AttemptToGetTypeFromAssembly(typeName, assembly);
	    }

	    private static Type AttemptToGetTypeFromAssembly(TypeName typeName, Assembly assembly)
	    {
	        var providerType = assembly.GetType(typeName.Name);
	        if (providerType != null)
	        {
	            new ProviderTypeResolver(assembly, typeName);
	        }
            return providerType;
	    }

        private class ProviderTypeResolver
        {
            private readonly Assembly assembly;
            private readonly TypeName typeName;

            public ProviderTypeResolver(Assembly assembly, TypeName typeName)
            {
                this.assembly = assembly;
                this.typeName = typeName;

                AppDomain.CurrentDomain.TypeResolve += TypeResolve;
            }

            private Assembly TypeResolve(object obj, ResolveEventArgs args)
            {
                if (args.Name.StartsWith(typeName.Name))
                {
                    return assembly;
                }
                return null;
            }
        }
	}
}