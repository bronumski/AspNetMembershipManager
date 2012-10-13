using System.Web.Security;

namespace AspNetMembershipManager.Web.Security
{
	public static class RoleProviderExtensions
	{
		 public static void AddUserToRole(this RoleProvider roleProvider, string userName, string role)
		 {
		 	roleProvider.AddUsersToRoles(new[] {userName}, new[] {role});
		 }
		
		public static void RemoveUserFromRole(this RoleProvider roleProvider, string userName, string role)
		 {
		 	roleProvider.RemoveUsersFromRoles(new[] {userName}, new[] {role});
		 }
	}
}