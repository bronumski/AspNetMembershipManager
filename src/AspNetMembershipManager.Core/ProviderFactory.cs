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
			                    GetProviderTypeFromBinAssemblies(providerSettings);

			if (!typeof(TProvider).IsAssignableFrom(providerType))
			{
				throw new Exception(string.Format("Invalid provider type '{0}', it is not assignable to '{1}", providerType, typeof(TProvider)));
			}

			var provider = (TProvider)Activator.CreateInstance(providerType);
			provider.Initialize(providerSettings.Name, new NameValueCollection(providerSettings.Parameters));

			return provider;
		}

		private static Type GetProviderTypeFromLoadedAssemblies(ProviderSettings providerSettings)
		{
			Type providerType = null;
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				providerType = assembly.GetType(providerSettings.Type);

				if (providerType != null) break;
			}
			return providerType;
		}

		private static Type GetProviderTypeFromBinAssemblies(ProviderSettings providerSettings)
		{
			var webConfigDirectory = new FileInfo(providerSettings.CurrentConfiguration.FilePath).Directory;

			var typeName = new TypeName(providerSettings.Type);

			if (typeName.AssemblyName == null)
			{
				var assemblies = webConfigDirectory.GetFiles("bin\\*.dll");

				foreach (var assemblyFile in assemblies)
				{
					var assembly = Assembly.LoadFile(assemblyFile.FullName);
					var providerType = assembly.GetType(typeName.Name);
					if (providerType != null)
					{
						return providerType;
					}
				}
			}
			else
			{
				var assemblyFile = webConfigDirectory.GetFiles(string.Format("bin\\{0}.dll", typeName.AssemblyName.Name)).SingleOrDefault();

				if (assemblyFile != null)
				{
					var assembly = Assembly.LoadFile(assemblyFile.FullName);
					var providerType = assembly.GetType(typeName.Name);
					if (providerType != null)
					{
						return providerType;
					}
				}
				else
				{
					Assembly assembly = Assembly.Load(typeName.AssemblyName);
					var providerType = assembly.GetType(typeName.Name);
					if (providerType != null)
					{
						return providerType;
					}
				}
			}
			throw new Exception(string.Format("Unable to load provider of type '{0}'", providerSettings.Type));
		}
	}

class TypeName
{
	public TypeName(string name)
	{
		var index = name.IndexOf(',');
		if (index > 0)
		{
			Name = name.Substring(0, index).Trim();

			AssemblyName = new AssemblyName(name.Substring(index + 1).Trim());
		}
		else
		{
			Name = name;	
		}
	}

	public string Name { get; private set; }

	public AssemblyName AssemblyName { get; private set; }
}
}