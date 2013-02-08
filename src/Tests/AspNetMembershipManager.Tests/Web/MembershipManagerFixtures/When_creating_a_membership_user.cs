using System;
using System.Web.Security;
using AspNetMembershipManager.Web.Security;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.Web.MembershipManagerFixtures
{
	[TestFixture]
	class When_creating_a_membership_user : AutoMockedSpecificationFor<MembershipManager, MembershipCreateStatus>
	{
		[Test]
		public void Should_return_membership_user()
		{
			Result.Should().Be(MembershipCreateStatus.Success);
		}

		protected override Func<MembershipCreateStatus> ActWithResult(MembershipManager classUnderTest)
		{
			MembershipCreateStatus membershipCreateStatus;

			GetDependency<MembershipProvider>()
				.CreateUser(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<object>(), out membershipCreateStatus)
				.Returns(x =>
				         	{
				         		x[7] = MembershipCreateStatus.Success;
				         		return Substitute.For<System.Web.Security.MembershipUser>();
				         	});

			return () => classUnderTest.CreateUser("Foo", string.Empty, string.Empty, string.Empty, string.Empty);
		}
	}
}