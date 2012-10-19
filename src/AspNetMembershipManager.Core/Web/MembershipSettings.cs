using System.Web.Security;

namespace AspNetMembershipManager.Web
{
	public class MembershipSettings : IMembershipSettings
	{
		public int MinRequiredPasswordLength
		{
			get { return Membership.MinRequiredPasswordLength; }
		}

		public bool RequiresQuestionAndAnswer
		{
			get { return Membership.RequiresQuestionAndAnswer; }
		}
	}
}