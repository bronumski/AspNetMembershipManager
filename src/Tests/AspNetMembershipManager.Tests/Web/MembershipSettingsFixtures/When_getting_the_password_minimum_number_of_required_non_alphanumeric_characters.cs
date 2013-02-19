using System;
using System.Web.Security;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.Web.MembershipSettingsFixtures
{
	class When_getting_the_password_minimum_number_of_required_non_alphanumeric_characters : When_getting_membership_settings<int>
	{
		protected override void SetupContext()
		{
			GetDependency<MembershipProvider>().MinRequiredNonAlphanumericCharacters.Returns(15);
		}

		[Test]
		public void Should_return_the_minimum_required_non_alphanumeric_characters_from_the_membership_provider()
		{
			Result.Should().Be(15);
		}

		protected override Func<int> ActWithResult(MembershipSettings classUnderTest)
		{
			return () => classUnderTest.MinRequiredNonAlphanumericCharacters;
		}
	}
}