using System;
using System.Collections.Generic;
using AspNetMembershipManager.Web.Security;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.Web.MembershipUserFixtures.When_getting_users_roles
{
    [TestFixture]
    class Given_roles_have_been_disabled : AutoMockedSpecificationFor<MembershipUser, IEnumerable<IRole>>
    {
    	private const string userName = "user name";

    	[Test]
        public void Should_return_no_roles()
        {
            Result.Should().BeEmpty();
        }

        protected override void SetupDependencies()
        {
            base.SetupDependencies();
            var roleManager = GetDependency<IRoleManager>();

            IEnumerable<string> roles = new[] { "Role 1", "Role 2" };

            roleManager.GetRolesForUser(userName).Returns(roles);
            roleManager.IsEnabled.Returns(false);
        }

        protected override Func<IEnumerable<IRole>> ActWithResult(MembershipUser classUnderTest)
        {
			classUnderTest.UserName.Returns(userName);
            return () => classUnderTest.Roles;
        }
    }
}