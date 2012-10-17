using System;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Provider;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Configuration;
using System.Web.Management;
using System.Web.Profile;
using System.Web.Security;
using AspNetMembershipManager.Collections.Specialized;

namespace AspNetMembershipManager
{
	public class WebProviderInitializer
	{
		private readonly IProviderFactory providerFactory;

		public WebProviderInitializer(IProviderFactory providerFactory)
		{
			this.providerFactory = providerFactory;
		}

		private const string SystemWebGroupName = "system.web";

        public Providers InitializeFromConfigurationFile(string configFilePath, bool createDatabases)
		{
			Configuration localConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

			Configuration remoteConfiguration = LoadRemoteConfiguration(configFilePath);

        	CopyRemoteConfigConnectionStringsToLocalConfig(remoteConfiguration, localConfiguration);

        	var remoteWebConfigurationGroup = (SystemWebSectionGroup)remoteConfiguration.GetSectionGroup(SystemWebGroupName);

        	((SystemWebSectionGroup) localConfiguration.GetSectionGroup(SystemWebGroupName))
        		.Profile.SectionInformation.SetRawXml(remoteWebConfigurationGroup.Profile.SectionInformation.GetRawXml());
        	
    		localConfiguration.Save();

            ConfigurationManager.RefreshSection("system.web/profile");

			if (remoteWebConfigurationGroup == null)
			{
				throw new Exception("Invalid configuration");
			}

			if (createDatabases)
			{
			    CreateDatabaseConnectionStringsForProviders(localConfiguration.ConnectionStrings, remoteWebConfigurationGroup);
			}

            var membershipProvider = LoadAndInitializeMembershipProvider(remoteWebConfigurationGroup);

        	var roleProvider = LoadAndInitializeRoleProvider(remoteWebConfigurationGroup);

        	var profileProvider = LoadAndInitializeProfileProvider(remoteWebConfigurationGroup);

        	return new Providers(membershipProvider, roleProvider, profileProvider);
		}

    	private ProfileProvider LoadAndInitializeProfileProvider(SystemWebSectionGroup remoteWebConfigurationGroup)
    	{
    		var profileConfiguration = remoteWebConfigurationGroup.Profile;

			if (profileConfiguration.Enabled)
			{
				if (profileConfiguration.Inherits != string.Empty && Type.GetType(profileConfiguration.Inherits) == null)
				{
					var profileTypeAssembly = GetAssemblyForProfileType(profileConfiguration);

					AppDomain.CurrentDomain.AssemblyResolve += (obj, args) =>
					                                           	{
					                                           		if (args.Name == profileTypeAssembly.FullName)
					                                           		{
					                                           			return profileTypeAssembly;
					                                           		}
					                                           		return null;
					                                           	};
					AppDomain.CurrentDomain.TypeResolve += (obj, args) =>
					                                       	{
					                                       		if (args.Name.StartsWith(profileConfiguration.Inherits))
					                                       		{
					                                       			return profileTypeAssembly;
					                                       		}
					                                       		return null;
					                                       	};
				}

				var defaultProfileProviderConfiguration =
					remoteWebConfigurationGroup.Profile.Providers[remoteWebConfigurationGroup.Profile.DefaultProvider];

				var profileProvider = providerFactory.CreateProviderFromConfig<ProfileProvider>(defaultProfileProviderConfiguration);

				RemoveReadonlyFlagFromProviderCollection(ProfileManager.Providers);

				ProfileManager.Providers.Clear();

				ProfileManager.Providers.Add(profileProvider);

	    		return profileProvider;
			}
    		return null;
    	}

		private static Assembly GetAssemblyForProfileType(ProfileSection profileSection)
		{
			var webConfigDirectory = new FileInfo(profileSection.CurrentConfiguration.FilePath).Directory;

			var assemblies = webConfigDirectory.GetFiles("bin\\*.dll");

			foreach (var assemblyFile in assemblies)
			{
				var assembly = Assembly.LoadFile(assemblyFile.FullName);
				var profileType = assembly.GetType(profileSection.Inherits);
				if (profileType != null)
				{
					return assembly;
				}
			}
			throw new Exception(string.Format("Unable to load profile type '{0}'", profileSection.Inherits));
		}

    	private MembershipProvider LoadAndInitializeMembershipProvider(SystemWebSectionGroup remoteWebConfigurationGroup)
    	{
    		var membershipProviderSection = remoteWebConfigurationGroup.Membership.Providers[remoteWebConfigurationGroup.Membership.DefaultProvider];

    		var membershipProvider = providerFactory.CreateProviderFromConfig<MembershipProvider>(membershipProviderSection);

    		RemoveReadonlyFlagFromProviderCollection(Membership.Providers);

    		Membership.Providers.Clear();
    		Membership.Providers.Add(membershipProvider);
    		
			return membershipProvider;
    	}

    	private RoleProvider LoadAndInitializeRoleProvider(SystemWebSectionGroup remoteWebConfigurationGroup)
    	{
    		var roleProviderSection = remoteWebConfigurationGroup.RoleManager.Providers[remoteWebConfigurationGroup.RoleManager.DefaultProvider];

    		var roleProvider = providerFactory.CreateProviderFromConfig<RoleProvider>(roleProviderSection);
    		return roleProvider;
    	}

    	private static void CopyRemoteConfigConnectionStringsToLocalConfig(Configuration remoteConfiguration, Configuration localConfiguration)
    	{
    		localConfiguration.ConnectionStrings.SectionInformation.SetRawXml(remoteConfiguration.ConnectionStrings.SectionInformation.GetRawXml());

    		localConfiguration.Save();

    		ConfigurationManager.RefreshSection("connectionStrings");
    	}

		private static void RemoveReadonlyFlagFromProviderCollection(ProviderCollection providerCollection)
		{
			typeof(ProviderCollection)
				.GetField("_ReadOnly", BindingFlags.NonPublic | BindingFlags.Instance)
				.SetValue(providerCollection, false);
		}

    	private static Configuration LoadRemoteConfiguration(string configFilePath)
    	{
    		var remoteConfigurationFileMap = new ExeConfigurationFileMap {ExeConfigFilename = configFilePath};

    		Configuration remoteConfiguration = ConfigurationManager.OpenMappedExeConfiguration(remoteConfigurationFileMap,
    		                                                                                    ConfigurationUserLevel.None);
    		return remoteConfiguration;
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