using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.Security;

namespace AspNetMembershipManager.Web.Security
{
	public class MembershipManager : IMembershipManager
	{
		private readonly MembershipProvider membershipProvider;

		public MembershipManager(MembershipProvider membershipProvider, MembershipSection membershipSection)
		{
			this.membershipProvider = membershipProvider;
		}

		public bool DeleteUser(string userName)
		{
			return membershipProvider.DeleteUser(userName, true);
		}

		public IEnumerable<MembershipUser> GetAllUsers()
		{
			int totalRecords;
			return membershipProvider.GetAllUsers(0, int.MaxValue, out totalRecords).Cast<MembershipUser>();
		}

		public void UpdateUser(MembershipUser user)
		{
			membershipProvider.UpdateUser(user);
		}

		public MembershipCreateStatus CreateUser(string username, string password, string emailAddress)
		{
			MembershipCreateStatus createStatus;
			membershipProvider.CreateUser(username, password, emailAddress, null, null, true, null, out createStatus);

			return createStatus;
		}

		public MembershipUser GetUser(string username)
		{
			return membershipProvider.GetUser(username, false);
		}
	}
}