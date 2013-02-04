using System.Web.Security;

namespace AspNetMembershipManager.Web
{
	public class MembershipPasswordGenerator : IPasswordGenerator
	{
		private readonly IMembershipSettings membershipSettings;

		public MembershipPasswordGenerator(IMembershipSettings membershipSettings)
		{
			this.membershipSettings = membershipSettings;
		}

		public string GeneratePassword()
		{
			return Membership.GeneratePassword(
				membershipSettings.MinRequiredPasswordLength,
				membershipSettings.MinRequiredNonAlphanumericCharacters);
		}
	}
}