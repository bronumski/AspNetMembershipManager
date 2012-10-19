
namespace AspNetMembershipManager.Web.Security
{
	public class MembershipUser : IUser
	{
		private readonly System.Web.Security.MembershipUser membershipUser;
		private readonly IMembershipManager membershipManager;

		public MembershipUser(System.Web.Security.MembershipUser membershipUser, IMembershipManager membershipManager)
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

		public void Delete()
		{
			membershipManager.DeleteUser(membershipUser.UserName);
		}

		public void Save()
		{
			membershipManager.UpdateUser(membershipUser);
		}
	}
}