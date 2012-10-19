using System;
using System.Collections.Generic;
using System.Linq;
using AspNetMembershipManager.Web.Security;
using NSubstitute;
using NUnit.Framework;
using MembershipUser = System.Web.Security.MembershipUser;

namespace AspNetMembershipManager.Web.MembershipUserFixtures.When_saving_a_user
{
    [TestFixture]
    class Given_the_user_has_a_new_role : AutoMockedSpecificationFor<Security.MembershipUser>
    {
        [Test]
        public void Should_add_user_to_new_role()
        {
            GetDependency<IRoleManager>().Received().AddUserToRole("user name", "Role 1");
        }

        [Test]
        public void Should_not_remove_user_from_any_roles()
        {
            GetDependency<IRoleManager>().DidNotReceive().RemoveUserFromRole(Arg.Any<string>(), Arg.Any<string>());
        }

        protected override void SetupDependencies()
        {
            base.SetupDependencies();
            GetDependency<MembershipUser>().UserName.Returns("user name");
			GetDependency<IRoleManager>().IsEnabled.Returns(true);
        }

        protected override Action Act(Security.MembershipUser classUnderTest)
        {
        	var role = Substitute.For<IRole>();
			role.Name.Returns("Role 1");
			classUnderTest.AddToRole(role);
            return classUnderTest.Save;
        }
    }

	[TestFixture]
    class Given_the_user_has_lost_a_role : AutoMockedSpecificationFor<Security.MembershipUser>
    {
        [Test]
        public void Should_not_add_user_to_new_role()
        {
            GetDependency<IRoleManager>().DidNotReceive().AddUserToRole(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void Should_remove_user_from_role()
        {
            GetDependency<IRoleManager>().Received().RemoveUserFromRole("user name", "Role 1");
        }

        protected override void SetupDependencies()
        {
            base.SetupDependencies();
            GetDependency<MembershipUser>().UserName.Returns("user name");
        	IEnumerable<string> userRoles = new[] {"Role 1"};
			GetDependency<IRoleManager>().GetRolesForUser("user name").Returns(userRoles);
			GetDependency<IRoleManager>().IsEnabled.Returns(true);
        }

        protected override Action Act(Security.MembershipUser classUnderTest)
        {
        	classUnderTest.RemoveFromRole(classUnderTest.Roles.Single(x => x.Name == "Role 1"));
            return classUnderTest.Save;
        }
    }
}