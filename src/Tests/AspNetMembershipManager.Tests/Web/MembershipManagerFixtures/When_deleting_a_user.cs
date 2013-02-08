using System;
using System.Web.Security;
using AspNetMembershipManager.Web.Security;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.Web.MembershipManagerFixtures
{
	[TestFixture]
	class When_deleting_a_user_succesfully : AutoMockedSpecificationFor<MembershipManager, bool>
	{
		[Test]
		public void Should_return_true()
		{
			Result.Should().BeTrue();
		}

		protected override Func<bool> ActWithResult(MembershipManager classUnderTest)
		{
			GetDependency<MembershipProvider>().DeleteUser("Foo", true).Returns(true);

			return () => classUnderTest.DeleteUser("Foo");
		}
	}
}