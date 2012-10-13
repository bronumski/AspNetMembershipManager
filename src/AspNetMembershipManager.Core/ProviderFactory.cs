using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Reflection;

namespace AspNetMembershipManager
{
	public class ProviderFactory : IProviderFactory
	{
		public TProvider CreateProviderFromConfig<TProvider>(ProviderSettings providerSettings) where TProvider : ProviderBase
		{
			Type providerType = null;
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				providerType = assembly.GetType(providerSettings.Type);

				if (providerType != null) break;
			}

			if (!typeof(TProvider).IsAssignableFrom(providerType))
			{
				throw new Exception(string.Format("Invalid provider type '{0}', it is not assignable to '{1}", providerType, typeof(TProvider)));
			}

			var provider = (TProvider)Activator.CreateInstance(providerType);
			provider.Initialize(providerSettings.Name, new NameValueCollection(providerSettings.Parameters));

			return provider;
		}
	}
}