namespace AspNetMembershipManager.Web
{
	public interface IMembershipSettings
	{
		int MinRequiredPasswordLength { get; }
		bool RequiresQuestionAndAnswer { get; }
        int MinRequiredNonAlphanumericCharacters { get; }
		bool CanResetPasswords { get; }
	}
}