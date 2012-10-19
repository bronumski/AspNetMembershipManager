using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.Security;

namespace AspNetMembershipManager.Web.Security
{
	public class MembershipManager : IMembershipManager
	{
		private readonly MembershipProvider membershipProvider;

		public MembershipManager(MembershipProvider membershipProvider)
		{
			this.membershipProvider = membershipProvider;
		}

		public bool DeleteUser(string userName)
		{
			return membershipProvider.DeleteUser(userName, true);
		}

		public IEnumerable<System.Web.Security.MembershipUser> GetAllUsers()
		{
			int totalRecords;
			return membershipProvider.GetAllUsers(0, int.MaxValue, out totalRecords).Cast<System.Web.Security.MembershipUser>();
		}

		public void UpdateUser(System.Web.Security.MembershipUser user)
		{
			membershipProvider.UpdateUser(user);
		}

		public MembershipCreateStatus CreateUser(string username, string password, string emailAddress, string passwordQuestion, string passwordQuestionAnswer)
		{
			MembershipCreateStatus createStatus;
			membershipProvider.CreateUser(username, password, emailAddress, passwordQuestion, passwordQuestionAnswer, true, null, out createStatus);

			return createStatus;
		}

		public System.Web.Security.MembershipUser GetUser(string username)
		{
			return membershipProvider.GetUser(username, false);
		}
	}
}