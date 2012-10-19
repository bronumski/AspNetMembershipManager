using System;
using System.Collections.Generic;
using AspNetMembershipManager.Web.Security;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.Web.ProviderManagerFixtures.When_creating_a_Role
{
    [TestFixture]
    class Given_roles_have_been_enabled : AutoMockedSpecificationFor<ProviderManagers, IRole>
    {
        private string roleName;

        [Test]
        public void Should_call_create_role_on_role_provider()
        {
            GetDependency<IRoleManager>().Received().CreateRole(roleName);
        }

        [Test]
        public void Should_return_the_new_role()
        {
            Result.Should().NotBeNull();
        }

        [Test]
        public void Should_have_the_correct_role_name()
        {
            Result.Name.Should().Be(roleName);
        }

        protected override void SetupParameters()
        {
            base.SetupParameters();

            roleName = "role name";
        }
        protected override void SetupDependencies()
        {
            base.SetupDependencies();
            var roleManager = GetDependency<IRoleManager>();

            IEnumerable<string> roles = new[] { roleName };

            roleManager.GetAllRoles().Returns(roles);

            roleManager.IsEnabled.Returns(true);
        }

        protected override Func<IRole>ActWithResult(ProviderManagers classUnderTest)
        {
            return () => classUnderTest.CreateRole(roleName);
        }
    }
}