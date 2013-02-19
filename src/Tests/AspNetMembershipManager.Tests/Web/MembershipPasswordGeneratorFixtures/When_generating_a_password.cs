using System;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.Web.MembershipPasswordGeneratorFixtures
{
	[TestFixture]
	class When_generating_a_password : AutoMockedSpecificationFor<MembershipPasswordGenerator, string>
	{
		protected override void SetupContext()
		{
			base.SetupContext();
			GetDependency<IMembershipSettings>().MinRequiredPasswordLength.Returns(20);
			GetDependency<IMembershipSettings>().MinRequiredNonAlphanumericCharacters.Returns(10);
		}

		[Test]
		public void Should_generate_a_password_of_the_required_minimum_length()
		{
			Result.Should().HaveLength(20);
		}

		[Test]
		public void Should_generate_a_password_with_the_correct_minimum_number_of_non_alphanumeric_characters()
		{
			char[] passwordAsCharArray = Result.ToCharArray();
			var nonAlphaNumericCharacters = passwordAsCharArray.Where(c => !char.IsLetterOrDigit(c));

			nonAlphaNumericCharacters.Count().Should().BeGreaterOrEqualTo(10);
		}

		protected override Func<string> ActWithResult(MembershipPasswordGenerator classUnderTest)
		{
			return () => classUnderTest.GeneratePassword();
		}
	}
}