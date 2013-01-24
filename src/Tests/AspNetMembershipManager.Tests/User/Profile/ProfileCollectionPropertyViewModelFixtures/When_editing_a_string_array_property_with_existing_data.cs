using System.Collections.ObjectModel;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.User.Profile.ProfileCollectionPropertyViewModelFixtures
{
	[TestFixture]
	internal class When_editing_a_string_array_property_with_existing_data
	{
		private ProfileCollectionPropertyViewModel profileCollectionProperty;
		private IProfileProperty profileProperty;

		[SetUp]
		public void SetUp()
		{
			profileProperty = Substitute.For<IProfileProperty>();
			profileProperty.PropertyName.Returns("Foo");
			profileProperty.PropertyType.Returns(typeof (string[]));
			profileProperty.PropertyValue = new[] {"Wibble", "Wobble"};
			profileCollectionProperty = new ProfileCollectionPropertyViewModel(profileProperty);
			((ObservableCollection<IProfileProperty>)profileCollectionProperty.Values)[0].PropertyValue = "Dibble";
		}

		[Test]
		public void Should_expose_all_the_data()
		{
			profileCollectionProperty.Values.Should().HaveCount(2);
		}

		[Test]
		public void Should_expose_the_edited_data()
		{
			profileCollectionProperty.Values.Should().Contain(x => ((string)x.PropertyValue) == "Dibble");
		}

		[Test]
		public void Should_not_change_the_original_data()
		{
			((string[])profileProperty.PropertyValue).Should().NotContain(new [] { "Dibble" });
		}
	}
}