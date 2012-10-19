using System;
using System.Web.Security;
using AspNetMembershipManager.Web.Security;
using FluentAssertions;
using NUnit.Framework;
using NSubstitute;
using MembershipUser = System.Web.Security.MembershipUser;

namespace AspNetMembershipManager.Web.ProviderManagerFixtures
{
	[TestFixture]
	class When_creating_a_user : AutoMockedSpecificationFor<ProviderManagers, IUser>
	{
		private string userName;
		private string password;
		private string emailAddress;

		[Test]
		public void Should_return_a_user_with_the_right_username()
		{
			Result.UserName.Should().Be(userName);
		}

		[Test]
		public void Should_call_create_with_username()
		{
			GetDependency<IMembershipManager>().Received().CreateUser(Arg.Is<string>(x => x == userName), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
		}

		[Test]
		public void Should_call_create_with_password()
		{
			GetDependency<IMembershipManager>().Received().CreateUser(Arg.Any<string>(), Arg.Is<string>(x => x == password), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
		}

		[Test]
		public void Should_call_create_with_email()
		{
			GetDependency<IMembershipManager>().Received().CreateUser(Arg.Any<string>(), Arg.Any<string>(), Arg.Is<string>(x => x == emailAddress), Arg.Any<string>(), Arg.Any<string>());
		}

		protected override void SetupDependencies()
		{
			base.SetupDependencies();
			MembershipUser expectedUser = CreateTestMembershipUser(userName);
			GetDependency<IMembershipManager>().GetUser(userName).Returns(expectedUser);
		}

		private static MembershipUser CreateTestMembershipUser(string userName)
		{
			var user = Substitute.For<MembershipUser>();
			user.UserName.Returns(userName);
			return user;
		}

		protected override void SetupParameters()
		{
			base.SetupParameters();
			userName = "user name";
			password = "password";
			emailAddress = "email address";
		}

		protected override Func<IUser> ActWithResult(ProviderManagers classUnderTest)
		{
			return () => classUnderTest.CreateUser(userName, password, emailAddress, null, null);
		}
	}
}