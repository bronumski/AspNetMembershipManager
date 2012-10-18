using System.Collections.Generic;
using System.Web.Security;
using AspNetMembershipManager.Web.Profile;
using AspNetMembershipManager.Web.Security;

namespace AspNetMembershipManager.Web
{
	public interface IProviderManagers
	{
		//IMembershipManager MembershipManager { get; }
		IRoleManager RoleManager { get; }
		IProfileManager ProfileManager { get; }

		IEnumerable<IUser> GetAllUsers();
		IUser CreateUser(string username, string password, string emailAddress);
	}
}