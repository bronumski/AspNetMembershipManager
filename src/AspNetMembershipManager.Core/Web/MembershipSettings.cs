using System.Web.Security;

namespace AspNetMembershipManager.Web
{
	public class MembershipSettings : IMembershipSettings
	{
	    private readonly MembershipProvider membershipProvider;

	    public MembershipSettings(MembershipProvider membershipProvider)
	    {
	        this.membershipProvider = membershipProvider;
	    }

	    public int MinRequiredPasswordLength
		{
            get { return membershipProvider.MinRequiredPasswordLength; }
		}

		public bool RequiresQuestionAndAnswer
		{
            get { return membershipProvider.RequiresQuestionAndAnswer; }
		}

        public int MinRequiredNonAlphanumericCharacters
        {
            get { return membershipProvider.MinRequiredNonAlphanumericCharacters; }
        }

		public bool CanResetPasswords
		{
			get { return membershipProvider.EnablePasswordReset && (! membershipProvider.RequiresQuestionAndAnswer); }
		}
	}
}