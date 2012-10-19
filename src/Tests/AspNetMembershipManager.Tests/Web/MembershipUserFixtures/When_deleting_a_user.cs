using System;
using AspNetMembershipManager.Web.Security;
using NUnit.Framework;
using NSubstitute;

namespace AspNetMembershipManager.Web.MembershipUserFixtures
{
	[TestFixture]
	class When_deleting_a_user : AutoMockedSpecificationFor<MembershipUser>
	{
		[Test]
		public void Should_call_delete_on_membership_provider()
		{
			GetDependency<IMembershipManager>().Received().DeleteUser(
				Arg.Is<string>(x => ReferenceEquals(x, ClassUnderTest.UserName)));
		}

		protected override Action Act(MembershipUser classUnderTest)
		{
			return classUnderTest.Delete;
		}
	}
}