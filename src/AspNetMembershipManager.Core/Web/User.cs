using System.Web.Security;
using AspNetMembershipManager.Web.Security;

namespace AspNetMembershipManager.Web
{
	public class User : IUser
	{
		private readonly MembershipUser membershipUser;
		private readonly IMembershipManager membershipManager;

		public User(MembershipUser membershipUser, IMembershipManager membershipManager)
		{
			this.membershipUser = membershipUser;
			this.membershipManager = membershipManager;
		}

		public string UserName
		{
			get { return membershipUser.UserName; }
		}

		public string EmailAddress
		{
			get { return membershipUser.Email; }
			set { membershipUser.Email = value; }
		}

		public bool Delete()
		{
			return membershipManager.DeleteUser(membershipUser.UserName);
		}

		public void Save()
		{
			membershipManager.UpdateUser(membershipUser);
		}
	}
}