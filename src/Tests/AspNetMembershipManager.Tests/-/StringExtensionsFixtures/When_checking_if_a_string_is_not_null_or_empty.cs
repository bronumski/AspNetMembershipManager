using FluentAssertions;
using NUnit.Framework;

namespace AspNetMembershipManager.StringExtensionsFixtures
{
	[TestFixture]
	class When_checking_if_a_string_is_not_null_or_empty
	{
		[Test]
		public void Should_return_true_for_a_non_empty_string()
		{
			"foo".IsNotNullOrEmpty().Should().BeTrue();
		}

		[Test]
		public void Should_return_false_for_an_empty_string()
		{
			string.Empty.IsNotNullOrEmpty().Should().BeFalse();
		}

		[Test]
		public void Should_return_false_for_a_null_string()
		{
			string.Empty.IsNotNullOrEmpty().Should().BeFalse();
		}
	}
}