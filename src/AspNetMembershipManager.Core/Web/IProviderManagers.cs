using System.Collections.Generic;
using AspNetMembershipManager.Web.Profile;

namespace AspNetMembershipManager.Web
{
	public interface IProviderManagers
	{
		IProfileManager ProfileManager { get; }

	    bool RolesEnabled { get; }

	    IEnumerable<IUser> GetAllUsers();

		IUser CreateUser(string username, string password, string emailAddress);

	    IEnumerable<IRole> GetAllRoles();

	    IRole CreateRole(string roleName);
	}
}