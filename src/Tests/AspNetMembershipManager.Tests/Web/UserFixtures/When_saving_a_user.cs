using System;
using System.Web.Security;
using AspNetMembershipManager.Web.Security;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.Web.UserFixtures
{
	[TestFixture]
	class When_saving_a_user : AutoMockedSpecificationFor<User>
	{
		[Test]
		public void Should_call_save_on_membership_provider()
		{
			GetDependency<IMembershipManager>().Received().UpdateUser(
				Arg.Is<MembershipUser>(x => x == GetDependency<MembershipUser>() ));
		}

		protected override Action Act(User classUnderTest)
		{
			return () => classUnderTest.Save();
		}
	}
}