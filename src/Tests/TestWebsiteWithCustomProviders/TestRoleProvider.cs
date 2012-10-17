using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

namespace TestWebsiteWithCustomProviders
{
    public class TestRoleProvider : RoleProvider
	{
		public static readonly IList<string> roles = new List<string> { "Admin" };
		private static readonly IDictionary<string, IList<string>> usersInRole = new Dictionary<string, IList<string>>();

		private IList<string> GetUsersRoles(string username)
		{
			if( !usersInRole.ContainsKey(username))
			{
				usersInRole.Add(username, new List<string>());
			}
			return usersInRole[username];
		}

		public override bool IsUserInRole(string username, string roleName)
		{
			return GetUsersRoles(username).Contains(roleName);
		}

		public override string[] GetRolesForUser(string username)
		{
			return GetUsersRoles(username).ToArray();
		}

		public override void CreateRole(string roleName)
		{
			roles.Add(roleName);
		}

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			return roles.Remove(roleName);
		}

		public override bool RoleExists(string roleName)
		{
			return roles.Contains(roleName);
		}

		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			foreach (var username in usernames)
			{
				if (!usersInRole.Keys.Contains(username))
				{
					usersInRole.Add(username, new List<string>(roleNames));
				}
				else
				{
					foreach (var role in roleNames.Except(usersInRole[username]))
					{
						usersInRole[username].Add(role);
					}
				}
			}
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			foreach (var username in usernames)
			{
				foreach (var role in roleNames.Intersect(GetUsersRoles(username)))
				{
					usersInRole[username].Remove(role);
				}
			}
		}

		public override string[] GetUsersInRole(string roleName)
		{
			return usersInRole.Where(x => x.Value.Contains(roleName)).Select(x => x.Key).ToArray();
		}

		public override string[] GetAllRoles()
		{
			return roles.ToArray();
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			return GetUsersInRole(roleName).Where(x => x == usernameToMatch).ToArray();
		}

		public override string ApplicationName
		{
			get { throw new System.NotImplementedException(); }
			set { throw new System.NotImplementedException(); }
		}
	}
}