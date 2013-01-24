using System.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.User.Profile.ProfileCollectionPropertyViewModelFixtures
{
	[TestFixture]
	internal class When_viewing_a_string_array_property_with_existing_data
	{
		private ProfileCollectionPropertyViewModel profileCollectionProperty;

		[SetUp]
		public void SetUp()
		{
			var profileProperty = Substitute.For<IProfileProperty>();
			profileProperty.PropertyName.Returns("Foo");
			profileProperty.PropertyType.Returns(typeof (string[]));
			profileProperty.PropertyValue = new[] {"Wibble", "Wobble"};
			profileCollectionProperty = new ProfileCollectionPropertyViewModel(profileProperty);
		}

		[Test]
		public void Should_expose_all_the_data()
		{
			profileCollectionProperty.Values.Should().HaveCount(2);
		}

		[Test]
		public void Should_expose_data_as_property_values()
		{
			profileCollectionProperty.Values.Should().Contain(x => ((string)x.PropertyValue) == "Wibble");
			profileCollectionProperty.Values.Should().Contain(x => ((string)x.PropertyValue) == "Wobble");
		}
	}
}