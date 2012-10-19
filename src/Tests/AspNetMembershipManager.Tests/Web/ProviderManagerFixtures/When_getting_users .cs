using System;
using System.Collections.Generic;
using System.Web.Security;
using AspNetMembershipManager.Web.Security;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using MembershipUser = System.Web.Security.MembershipUser;

namespace AspNetMembershipManager.Web.ProviderManagerFixtures
{
	[TestFixture]
	class When_getting_users : AutoMockedSpecificationFor<ProviderManagers, IEnumerable<IUser>>
	{
		[Test]
		public void Should_return_all_the_users_from_the_membership_provider()
		{
			Result.Should().HaveCount(2);
		}

		protected override void SetupDependencies()
		{
			base.SetupDependencies();
			var membershipProvider = GetDependency<IMembershipManager>();

			IEnumerable<MembershipUser> users = new[] {CreateTestMembershipUser(), CreateTestMembershipUser()};

			membershipProvider.GetAllUsers().Returns(users);
		}

		private static MembershipUser CreateTestMembershipUser()
		{
			var user = Substitute.For<MembershipUser>();
			user.UserName.Returns(Guid.NewGuid().ToString());
			return user;
		}

		protected override Func<IEnumerable<IUser>> ActWithResult(ProviderManagers classUnderTest)
		{
			return classUnderTest.GetAllUsers;
		}
	}
}