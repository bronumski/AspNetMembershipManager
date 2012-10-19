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
		private readonly DirectoryInfo binDirectory;

		public ProviderFactory(DirectoryInfo binDirectory)
		{
			this.binDirectory = binDirectory;
		}

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
            return Type.GetType(providerSettings.Type) ??
				Assembly.Load(new AssemblyName("System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")).GetType(providerSettings.Type);
		}

	    private Type GetProviderTypeFromExternalAssemblies(ProviderSettings providerSettings)
		{
			var typeName = new TypeName(providerSettings.Type);

	        return
	            AttemptToGetTypeFromSpecificAssemblyInBin(typeName) ??
	            AttemptToGetTypeFromAnyAssemblyInBin(typeName) ??
	            AttemptToGetTypeFromGacAssembly(typeName);
		}

        private Type AttemptToGetTypeFromSpecificAssemblyInBin(TypeName typeName)
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

	    private Type AttemptToGetTypeFromAnyAssemblyInBin(TypeName typeName)
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
                AppDomain.CurrentDomain.AssemblyResolve += AssemblyResolve;
            }

            private Assembly TypeResolve(object obj, ResolveEventArgs args)
            {
                if (args.Name.StartsWith(typeName.Name))
                {
                    return assembly;
                }
                return null;
            }

			private Assembly AssemblyResolve(object obj, ResolveEventArgs args)
			{
				if (args.Name == assembly.FullName)
				{
					return assembly;
				}
				return null;
			}
        }
	}
}