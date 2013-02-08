using System;
using System.Web.Security;
using AspNetMembershipManager.Web.Security;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.Web.RoleManagerFixtures
{
	class When_removing_a_user_from_a_role : RoleManagerFixtureBase<object>
	{
		[Test]
		public void Should_call_add_user_to_role_method()
		{
			GetDependency<RoleProvider>().Received().RemoveUsersFromRoles(Arg.Is<string[]>(x => x[0] == "Foo"), Arg.Is<string[]>(x => x[0] == "Wibble"));
		}

		protected override Func<object> ActWithResult(RoleManager classUnderTest)
		{
			return () =>
			       	{
			       		classUnderTest.RemoveUserFromRole("Foo", "Wibble");
			       		return null;
			       	};
		}
	}
}