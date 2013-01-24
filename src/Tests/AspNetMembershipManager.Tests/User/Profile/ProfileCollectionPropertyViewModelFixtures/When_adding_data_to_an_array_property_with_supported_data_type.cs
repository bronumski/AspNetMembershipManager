using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.User.Profile.ProfileCollectionPropertyViewModelFixtures
{
	[TestFixture]
	internal class When_adding_data_to_an_array_property_with_supported_data_type : SupportedDataTypeFixturesBase
	{
		private ProfileCollectionPropertyViewModel profileCollectionProperty;
		private IProfileProperty profileProperty;


		[TestCaseSource("DataTypes")]
		public void Should_expose_all_the_data(Type dataType, bool isSupported)
		{
			profileProperty = Substitute.For<IProfileProperty>();
			profileProperty.PropertyType.Returns(Array.CreateInstance(dataType, 0).GetType());

			profileCollectionProperty = new ProfileCollectionPropertyViewModel(profileProperty);

			profileCollectionProperty.AddNewItem();

			profileCollectionProperty.Values.Should().HaveCount(isSupported ? 1 : 0);
		}
	}
}