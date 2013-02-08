using System;
using System.Web.Configuration;
using AspNetMembershipManager.Web.Security;
using FluentAssertions;
using NUnit.Framework;

namespace AspNetMembershipManager.Web.RoleManagerFixtures
{
	class When_checking_if_roles_are_enabled : RoleManagerFixtureBase<bool>
	{
		[Test]
		public void Should_get_the_status_from_the_role_configuration_section()
		{
			Result.Should().BeTrue();
		}

		protected override Func<bool> ActWithResult(RoleManager classUnderTest)
		{
			GetDependency<RoleManagerSection>().Enabled = true;
			return () => classUnderTest.IsEnabled;
		}
	}
}