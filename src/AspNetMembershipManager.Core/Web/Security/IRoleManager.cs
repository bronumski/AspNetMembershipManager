using System.Collections.Generic;

namespace AspNetMembershipManager.Web.Security
{
	public interface IRoleManager
	{
		bool IsEnabled { get; }
		bool DeleteRole(string name);
		IEnumerable<string> GetAllRoles();
		void CreateRole(string roleName);
		IEnumerable<string> GetRolesForUser(string userName);
		bool IsUserInRole(string userName, string roleName);
		void AddUserToRole(string userName, string roleName);
		void RemoveUserFromRole(string userName, string roleName);
        IEnumerable<string> GetUsersInRole(string roleName);
	}
}