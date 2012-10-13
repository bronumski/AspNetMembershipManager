using System.Collections.Generic;
using System.Collections.Specialized;
using FluentAssertions;
using NUnit.Framework;

namespace AspNetMembershipManager.Collections.Specialized.NameValueCollectionExtensionsFixtures
{
	[TestFixture]
	class When_converting_a_collection_to_a_dictionary
	{
		[Test]
		public void Should_contain_all_the_name_value_pairs_from_the_collection_as_key_value_pairs()
		{
			var collection = new NameValueCollection { { "foo", "bar" }, { "wibble", "wobble" } };

			IDictionary<string, string> dictionary = collection.ToDictionary();

			dictionary.Should().Contain("foo", "bar").And.Contain("wibble", "wobble");
		}
	}
}