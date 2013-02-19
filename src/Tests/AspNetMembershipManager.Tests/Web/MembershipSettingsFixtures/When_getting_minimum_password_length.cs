using System;
using System.Web.Security;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.Web.MembershipSettingsFixtures
{
	class When_getting_minimum_password_length : When_getting_membership_settings<int>
	{
		protected override void SetupContext()
		{
			GetDependency<MembershipProvider>().MinRequiredPasswordLength.Returns(25);
		}

		[Test]
		public void Should_return_minimum_poassword_length_from_membership_provider()
		{
			Result.Should().Be(25);
		}

		protected override Func<int> ActWithResult(MembershipSettings classUnderTest)
		{
			return () => classUnderTest.MinRequiredPasswordLength;
		}
	}
}