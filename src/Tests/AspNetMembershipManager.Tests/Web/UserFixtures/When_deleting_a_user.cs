using System;
using AspNetMembershipManager.Web.Security;
using NUnit.Framework;
using NSubstitute;

namespace AspNetMembershipManager.Web.UserFixtures
{
	[TestFixture]
	class When_deleting_a_user : AutoMockedSpecificationFor<User, bool>
	{
		[Test]
		public void Should_call_delete_on_membership_provider()
		{
			GetDependency<IMembershipManager>().Received().DeleteUser(
				Arg.Is<string>(x => ReferenceEquals(x, ClassUnderTest.UserName)));
		}

		protected override Func<bool> ActWithResult(User classUnderTest)
		{
			return () => classUnderTest.Delete();
		}
	}
}