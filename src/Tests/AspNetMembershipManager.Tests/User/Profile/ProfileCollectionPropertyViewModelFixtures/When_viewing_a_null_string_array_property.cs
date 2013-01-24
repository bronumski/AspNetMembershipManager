using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.User.Profile.ProfileCollectionPropertyViewModelFixtures
{
	[TestFixture]
	class When_viewing_a_null_string_array_property
	{
		private ProfileCollectionPropertyViewModel profileCollectionProperty;

		[SetUp]
		public void SetUp()
		{
			var profileProperty = Substitute.For<IProfileProperty>();
			profileProperty.PropertyName.Returns("Foo");
			profileProperty.PropertyType.Returns(typeof(string[]));
			profileCollectionProperty = new ProfileCollectionPropertyViewModel(profileProperty);
		}

		[Test]
		public void Should_create_an_empty_set_of_data()
		{
			profileCollectionProperty.Values.Should().BeEmpty();
		}

		[Test]
		public void Should_expose_the_property_name()
		{
			profileCollectionProperty.Name.Should().Be("Foo");
		}

		[Test]
		public void Should_expose_the_property_type()
		{
			profileCollectionProperty.Type.Should().Be(typeof(string[]));
		}

		[Test]
		public void Should_show_that_the_array_type_is_supported()
		{
			profileCollectionProperty.IsSupportedDataType.Should().BeTrue();
		}
	}
}