using System;
using System.Collections;
using AspNetMembershipManager.Web.Security;
using Castle.MicroKernel.Registration;
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
                Arg.Is<string>(x => x == "role 1"));
		}

		protected override ComponentRegistration<Security.Role> RegisterClassUnderTest(ComponentRegistration<Security.Role> componentRegistration)
		{
			return base.RegisterClassUnderTest(componentRegistration).DependsOn(new Hashtable {  { "name", "role 1" } });
		}

        protected override Action Act(Security.Role classUnderTest)
		{
			return classUnderTest.Delete;
		}
	}
}