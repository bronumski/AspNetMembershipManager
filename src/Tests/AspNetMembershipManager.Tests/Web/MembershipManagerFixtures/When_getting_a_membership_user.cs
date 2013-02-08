using System;
using System.Web.Security;
using AspNetMembershipManager.Web.Security;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.Web.MembershipManagerFixtures
{
	[TestFixture]
	class When_getting_a_membership_user : AutoMockedSpecificationFor<MembershipManager, System.Web.Security.MembershipUser>
	{
		[Test]
		public void Should_return_membership_user()
		{
			Result.Should().NotBeNull();
		}

		protected override Func<System.Web.Security.MembershipUser> ActWithResult(MembershipManager classUnderTest)
		{
			GetDependency<MembershipProvider>().GetUser("Foo", false).Returns(Substitute.For<System.Web.Security.MembershipUser>());

			return () => classUnderTest.GetUser("Foo");
		}
	}
}