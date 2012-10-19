using System.Collections.Generic;

namespace AspNetMembershipManager.Web
{
	public interface IProviderManagers
	{
		IMembershipSettings MembershipSettings { get; }

	    bool RolesEnabled { get; }

	    IEnumerable<IUser> GetAllUsers();

		IUser CreateUser(string username, string password, string emailAddress, string passwordQuestion, string passwordQuestionAnswer);

	    IEnumerable<IRole> GetAllRoles();

	    IRole CreateRole(string roleName);
	}
}