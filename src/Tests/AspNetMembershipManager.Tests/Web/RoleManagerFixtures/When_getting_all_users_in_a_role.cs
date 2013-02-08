using System;
using System.Collections.Generic;
using System.Web.Security;
using AspNetMembershipManager.Web.Security;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.Web.RoleManagerFixtures
{
	class When_getting_all_users_in_a_role : RoleManagerFixtureBase<IEnumerable<string>>
	{
		[Test]
		public void Should_contain_two_roles()
		{
			Result.Should().HaveCount(2);
		}

		[Test]
		public void Should_contain_Foo_user()
		{
			Result.Should().Contain("Foo");
		}

		[Test]
		public void Should_contain_Bar_user()
		{
			Result.Should().Contain("Bar");
		}

		protected override Func<IEnumerable<string>> ActWithResult(RoleManager classUnderTest)
		{
			GetDependency<RoleProvider>().GetUsersInRole("Wibble").Returns(new [] { "Foo", "Bar" });

			return () => classUnderTest.GetUsersInRole("Wibble");
		}
	}
}