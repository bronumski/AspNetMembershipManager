using NUnit.Framework;

namespace AspNetMembershipManager.StringExtensionsFixtures
{
	[TestFixture]
	class When_formating_a_string
	{
		[Test]
		public void Should_format_a_given_string_with_supplied_parameters()
		{
			"wibble {0} foo {1}".Composite("wobble", "bar");
		}
	}
}