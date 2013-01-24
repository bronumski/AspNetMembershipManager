using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.User.Profile.ProfileCollectionPropertyViewModelFixtures
{
	[TestFixture]
	internal class When_getting_supported_data_types : SupportedDataTypeFixturesBase
	{
		private ProfileCollectionPropertyViewModel profileCollectionProperty;
		private IProfileProperty profileProperty;


		[TestCaseSource("DataTypes")]
		public void Should_expose_all_the_data(Type dataType, bool isSupported)
		{
			profileProperty = Substitute.For<IProfileProperty>();
			profileProperty.PropertyType.Returns(Array.CreateInstance(dataType, 0).GetType());

			profileCollectionProperty = new ProfileCollectionPropertyViewModel(profileProperty);

			profileCollectionProperty.IsSupportedDataType.Should().Be(isSupported);
		}
	}
}