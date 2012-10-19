using System;
using AspNetMembershipManager.Web.Security;
using NUnit.Framework;
using NSubstitute;

namespace AspNetMembershipManager.Web.RoleFixtures
{
	[TestFixture]
	class When_deleting_a_role : AutoMockedSpecificationFor<Security.Role>
	{
		[Test]
		public void Should_call_delete_on_role_provider()
		{
			GetDependency<IRoleManager>().Received().DeleteRole(
                Arg.Is<string>(x => ReferenceEquals(x, ClassUnderTest.Name)));
		}

        protected override Action Act(Security.Role classUnderTest)
		{
			return classUnderTest.Delete;
		}
	}
}