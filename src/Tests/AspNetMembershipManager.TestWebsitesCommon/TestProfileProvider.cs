using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Profile;

namespace AspNetMembershipManager.TestWebsitesCommon
{
	public class TestProfileProvider : ProfileProvider
	{
		private static readonly IDictionary<string, IDictionary<string, object>> usersProfiles = new Dictionary<string, IDictionary<string, object>>();

		public override string ApplicationName { get; set; }

		private static IDictionary<string, object> GetUsersProfile(SettingsContext context)
		{
			var username = (string) context["UserName"];

			if (!usersProfiles.ContainsKey(username))
			{
				usersProfiles.Add(username, new Dictionary<string, object>());
			}
			return usersProfiles[username];
		}

		public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection ppc)
		{
			var usersProfile = GetUsersProfile(context);

			var svc = new SettingsPropertyValueCollection();

			foreach (SettingsProperty prop in ppc)
			{
				var pv = new SettingsPropertyValue(prop);
				if (! usersProfile.ContainsKey(prop.Name))
				{
					usersProfile.Add(prop.Name, null);
				}
	
				pv.PropertyValue = usersProfile[prop.Name];
				svc.Add(pv);
			}

			return svc;
		}

		public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection ppvc)
		{
			var usersProfile = GetUsersProfile(context);

			foreach (SettingsPropertyValue pv in ppvc)
			{
				if (!usersProfile.ContainsKey(pv.Name))
				{
					usersProfile.Add(pv.Name, pv.PropertyValue);
				}
				else
				{
					usersProfile[pv.Name] = pv.PropertyValue;
				}
			}
		}

		public override int DeleteProfiles(ProfileInfoCollection profiles)
		{
			throw new NotImplementedException();
		}

		public override int DeleteProfiles(string[] usernames)
		{
			throw new NotImplementedException();
		}

		public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
		{
			throw new NotImplementedException();
		}

		public override ProfileInfoCollection FindProfilesByUserName(
			ProfileAuthenticationOption authenticationOption,
			string usernameToMatch,
			int pageIndex,
			int pageSize,
			out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override ProfileInfoCollection FindInactiveProfilesByUserName(
			ProfileAuthenticationOption authenticationOption,
			string usernameToMatch,
			DateTime userInactiveSinceDate,
			int pageIndex,
			int pageSize,
			out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override ProfileInfoCollection GetAllProfiles(
			ProfileAuthenticationOption authenticationOption,
			int pageIndex,
			int pageSize,
			out int totalRecords)
		{
			throw new NotImplementedException();
		}


		public override ProfileInfoCollection GetAllInactiveProfiles(
			ProfileAuthenticationOption authenticationOption,
			DateTime userInactiveSinceDate,
			int pageIndex,
			int pageSize,
			out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override int GetNumberOfInactiveProfiles(
			ProfileAuthenticationOption authenticationOption,
			DateTime userInactiveSinceDate)
		{
			throw new NotImplementedException();
		}
	}
}