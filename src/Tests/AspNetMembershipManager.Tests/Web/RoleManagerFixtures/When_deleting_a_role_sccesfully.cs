using System;
using System.Web.Security;
using AspNetMembershipManager.Web.Security;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.Web.RoleManagerFixtures
{
	class When_deleting_a_role_sccesfully : RoleManagerFixtureBase<bool>
	{
		[Test]
		public void Should_return_a_positive_result()
		{
			Result.Should().BeTrue();
		}

		protected override Func<bool> ActWithResult(RoleManager classUnderTest)
		{
			GetDependency<RoleProvider>().DeleteRole("Foo", false).Returns(true);

			return () => classUnderTest.DeleteRole("Foo");
		}
	}
}