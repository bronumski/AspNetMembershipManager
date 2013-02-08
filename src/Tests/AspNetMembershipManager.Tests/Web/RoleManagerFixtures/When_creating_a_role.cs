using System;
using System.Web.Security;
using AspNetMembershipManager.Web.Security;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.Web.RoleManagerFixtures
{
	class When_creating_a_role : RoleManagerFixtureBase<object>
	{
		[Test]
		public void Should_call_create_role_method()
		{
			GetDependency<RoleProvider>().Received().CreateRole("Foo");
		}

		protected override Func<object> ActWithResult(RoleManager classUnderTest)
		{
			return () =>
			       	{
			       		classUnderTest.CreateRole("Foo");
			       		return null;
			       	};
		}
	}
}