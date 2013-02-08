using System;
using System.Web.Security;
using AspNetMembershipManager.Web.Security;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.Web.RoleManagerFixtures
{
	class When_checking_if_a_user_is_in_a_role_that_they_are_assigned_to : RoleManagerFixtureBase<bool>
	{
		[Test]
		public void Should_return_a_positive_result()
		{
			Result.Should().BeTrue();
		}

		protected override Func<bool> ActWithResult(RoleManager classUnderTest)
		{
			GetDependency<RoleProvider>().IsUserInRole("Foo", "Wibble").Returns(true);

			return () => classUnderTest.IsUserInRole("Foo", "Wibble");
		}
	}
}