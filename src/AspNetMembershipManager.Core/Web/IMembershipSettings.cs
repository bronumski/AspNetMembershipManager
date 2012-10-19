namespace AspNetMembershipManager.Web
{
	public interface IMembershipSettings
	{
		int MinRequiredPasswordLength { get; }
		bool RequiresQuestionAndAnswer { get; }
	}
}