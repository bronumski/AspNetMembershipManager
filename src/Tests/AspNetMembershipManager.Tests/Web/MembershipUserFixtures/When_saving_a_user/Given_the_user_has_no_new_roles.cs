using System;
using AspNetMembershipManager.Web.Security;
using NSubstitute;
using NUnit.Framework;
using MembershipUser = System.Web.Security.MembershipUser;

namespace AspNetMembershipManager.Web.MembershipUserFixtures.When_saving_a_user
{
	[TestFixture]
	class Given_the_user_has_no_new_roles : AutoMockedSpecificationFor<Security.MembershipUser>
	{
        [Test]
        public void Should_not_add_user_to_any_roles()
        {
            GetDependency<IRoleManager>().DidNotReceive().AddUserToRole(Arg.Any<string>(), Arg.Any<string>());
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
        }

		protected override Action Act(Security.MembershipUser classUnderTest)
		{
			return classUnderTest.Save;
		}
	}
}