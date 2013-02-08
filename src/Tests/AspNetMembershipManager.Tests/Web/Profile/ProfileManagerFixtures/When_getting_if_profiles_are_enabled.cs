using System.Web.Configuration;
using FluentAssertions;
using NUnit.Framework;

namespace AspNetMembershipManager.Web.Profile.ProfileManagerFixtures
{
	[TestFixture]
	class When_getting_if_profiles_are_enabled
	{
		[Test]
		public void Should_return_the_state_from_the_profile_configuration()
		{
			var profileSection = new ProfileSection {Enabled = true};

			var profileManager = new ProfileManager(profileSection);

			profileManager.IsEnabled.Should().BeTrue();
		}
	}
}