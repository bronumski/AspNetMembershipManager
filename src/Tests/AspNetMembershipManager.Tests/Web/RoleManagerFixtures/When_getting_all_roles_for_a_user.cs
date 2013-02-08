using System;
using System.Collections.Generic;
using System.Web.Security;
using AspNetMembershipManager.Web.Security;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.Web.RoleManagerFixtures
{
	class When_getting_all_roles_for_a_user : RoleManagerFixtureBase<IEnumerable<string>>
	{
		[Test]
		public void Should_contain_two_roles()
		{
			Result.Should().HaveCount(2);
		}

		[Test]
		public void Should_contain_Wibble_role()
		{
			Result.Should().Contain("Wibble");
		}

		[Test]
		public void Should_contain_Wobble_role()
		{
			Result.Should().Contain("Wobble");
		}

		protected override Func<IEnumerable<string>> ActWithResult(RoleManager classUnderTest)
		{
			GetDependency<RoleProvider>().GetRolesForUser("Foo").Returns(new [] { "Wibble", "Wobble" });

			return () => classUnderTest.GetRolesForUser("Foo");
		}
	}
}