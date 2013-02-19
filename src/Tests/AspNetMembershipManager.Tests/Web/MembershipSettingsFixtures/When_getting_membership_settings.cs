using NUnit.Framework;

namespace AspNetMembershipManager.Web.MembershipSettingsFixtures
{
	[TestFixture]
	abstract class When_getting_membership_settings<TValue> : AutoMockedSpecificationFor<MembershipSettings, TValue>
	{
	}
}