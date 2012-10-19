using System;
using System.Collections.Generic;
using AspNetMembershipManager.Web.Security;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.Web.MembershipUserFixtures.When_getting_users_roles
{
    [TestFixture]
    class Given_roles_have_been_enabled : AutoMockedSpecificationFor<MembershipUser, IEnumerable<IRole>>
    {
		private const string userName = "user name";

        [Test]
        public void Should_return_all_the_roles_from_the_membership_provider()
        {
            Result.Should().HaveCount(2);
        }

        protected override void SetupDependencies()
        {
            base.SetupDependencies();
            var roleManager = GetDependency<IRoleManager>();

            IEnumerable<string> roles = new[] { "Role 1", "Role 2" };

            roleManager.GetRolesForUser(userName).Returns(roles);
            roleManager.IsEnabled.Returns(true);

			GetDependency<System.Web.Security.MembershipUser>().UserName.Returns(userName);
        }

        protected override Func<IEnumerable<IRole>> ActWithResult(MembershipUser classUnderTest)
        {
            return () => classUnderTest.Roles;
        }
    }
}