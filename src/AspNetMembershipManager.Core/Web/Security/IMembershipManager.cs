using System.Collections.Generic;
using System.Web.Security;

namespace AspNetMembershipManager.Web.Security
{
	public interface IMembershipManager
	{
		bool DeleteUser(string userName);
		IEnumerable<MembershipUser> GetAllUsers();
		void UpdateUser(MembershipUser user);
		MembershipCreateStatus CreateUser(string username, string password, string emailAddress);
		MembershipUser GetUser(string username);
	}
}