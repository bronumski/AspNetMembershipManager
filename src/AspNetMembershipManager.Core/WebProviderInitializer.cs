using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web.Configuration;
using System.Web.Management;
using System.Web.Security;
using AspNetMembershipManager.Collections.Specialized;

namespace AspNetMembershipManager
{
    public class WebProviderInitializer
	{
		private const string SystemWebGroupName = "system.web";

        public Providers InitializeFromConfigurationFile(string configFilePath, bool createDatabases)
		{
			Configuration localConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

			var remoteConfigurationFileMap = new ExeConfigurationFileMap {ExeConfigFilename = configFilePath};

			Configuration remoteConfiguration = ConfigurationManager.OpenMappedExeConfiguration(remoteConfigurationFileMap, ConfigurationUserLevel.None);

			localConfiguration.ConnectionStrings.SectionInformation.SetRawXml(remoteConfiguration.ConnectionStrings.SectionInformation.GetRawXml());

            var remoteWebConfigurationGroup = (SystemWebSectionGroup)remoteConfiguration.GetSectionGroup(SystemWebGroupName);

            localConfiguration.Save();
            
            ConfigurationManager.RefreshSection("connectionStrings");

			if (createDatabases)
			{
			    CreateDatabaseConnectionStringsForProviders(localConfiguration.ConnectionStrings, remoteWebConfigurationGroup);
			}

            var membershipProviderSection = remoteWebConfigurationGroup.Membership.Providers[remoteWebConfigurationGroup.Membership.DefaultProvider];

            var membershipProvider = GetProviderFromConfig<MembershipProvider>(membershipProviderSection);
            typeof(ProviderCollection).GetField("_ReadOnly", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(Membership.Providers, false);
            Membership.Providers.Clear();
            Membership.Providers.Add(membershipProvider);

            var roleProviderSection = remoteWebConfigurationGroup.RoleManager.Providers[remoteWebConfigurationGroup.RoleManager.DefaultProvider];
            
            var roleProvider = GetProviderFromConfig<RoleProvider>(roleProviderSection);

		    return new Providers(membershipProvider, roleProvider);
		}

        private static TProvider GetProviderFromConfig<TProvider>(ProviderSettings providerSettings) where TProvider : ProviderBase
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


		private void CreateDatabaseConnectionStringsForProviders(ConnectionStringsSection connectionStringsSection, SystemWebSectionGroup webSectionGroup)
		{
			var connectionStringNames = GetDatabaseConnectionStringsForProvider(webSectionGroup.RoleManager.Providers)
				.Union(GetDatabaseConnectionStringsForProvider(webSectionGroup.Membership.Providers))
				.Union(GetDatabaseConnectionStringsForProvider(webSectionGroup.Profile.Providers))
				.Distinct()
				.Intersect(connectionStringsSection.ConnectionStrings.Cast<ConnectionStringSettings>().Select(x => x.Name));

			var connectionStrings = connectionStringNames.Select(connectionStringName => connectionStringsSection.ConnectionStrings[connectionStringName].ConnectionString);

			foreach (var connectionString in connectionStrings)
			{
				var connection = new SqlConnectionStringBuilder(connectionString);
				var database = connection.InitialCatalog;
				connection.InitialCatalog = string.Empty;
                SqlServices.Install(database, SqlFeatures.All, connection.ConnectionString);
			}
		}

		private IEnumerable<string> GetDatabaseConnectionStringsForProvider(ProviderSettingsCollection providerConfigs)
		{
			return providerConfigs.Cast<ProviderSettings>()
				.SelectMany(x => x.Parameters.ToDictionary())
				.Where(x => x.Key == "connectionStringName")
				.Select(x => x.Value).Distinct();
		}
	}
}