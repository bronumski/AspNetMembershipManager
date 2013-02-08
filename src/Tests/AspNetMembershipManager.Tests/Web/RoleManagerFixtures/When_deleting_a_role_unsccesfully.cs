using System;
using System.Web.Security;
using AspNetMembershipManager.Web.Security;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.Web.RoleManagerFixtures
{
	class When_deleting_a_role_unsccesfully : RoleManagerFixtureBase<bool>
	{
		[Test]
		public void Should_return_a_negative_result()
		{
			Result.Should().BeFalse();
		}

		protected override Func<bool> ActWithResult(RoleManager classUnderTest)
		{
			GetDependency<RoleProvider>().DeleteRole("Foo", false).Returns(false);

			return () => classUnderTest.DeleteRole("Foo");
		}
	}
}