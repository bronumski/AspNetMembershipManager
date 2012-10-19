using System;
using System.Collections.Generic;
using AspNetMembershipManager.Web.Security;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.Web.ProviderManagerFixtures.When_getting_All_Roles
{
    [TestFixture]
    class Given_roles_have_been_enabled : AutoMockedSpecificationFor<ProviderManagers, IEnumerable<IRole>>
    {
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

            roleManager.GetAllRoles().Returns(roles);
            roleManager.IsEnabled.Returns(true);
        }

        protected override Func<IEnumerable<IRole>> ActWithResult(ProviderManagers classUnderTest)
        {
            return classUnderTest.GetAllRoles;
        }
    }
}