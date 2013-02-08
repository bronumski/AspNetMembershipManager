using System;
using System.Collections.Generic;
using System.Web.Security;
using AspNetMembershipManager.Web.Security;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using MembershipUser = System.Web.Security.MembershipUser;

namespace AspNetMembershipManager.Web.MembershipManagerFixtures
{
	[TestFixture]
	class When_getting_all_membership_users : AutoMockedSpecificationFor<MembershipManager, IEnumerable<MembershipUser>>
	{
		[Test]
		public void Should_return_a_collection_of_two_users()
		{
			Result.Should().HaveCount(2);
		}

		protected override Func<IEnumerable<System.Web.Security.MembershipUser>> ActWithResult(MembershipManager classUnderTest)
		{
			int totalUsers;

			GetDependency<MembershipProvider>()
				.GetAllUsers(Arg.Any<int>(), Arg.Any<int>(), out totalUsers)
				.Returns(x =>
				         	{
				         		var user1 = Substitute.For<System.Web.Security.MembershipUser>();
				         		user1.UserName.Returns("Foo");
								
				         		var user2 = Substitute.For<System.Web.Security.MembershipUser>();
				         		user2.UserName.Returns("Bar");

				         		totalUsers = int.MaxValue;

				         		return new MembershipUserCollection { user1, user2 };
				         	});

			return () => classUnderTest.GetAllUsers();
		}
	}
}