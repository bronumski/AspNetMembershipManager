using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace AspNetMembershipManager.User.ProfilePropertyTemplateSelectorFixtures
{
	[TestFixture]
	class When_getting_a_data_template
	{
		private static readonly DataTemplate NumberTemplate = Substitute.For<DataTemplate>();
		private static readonly DataTemplate BooleanTemplate = Substitute.For<DataTemplate>();
		private static readonly DataTemplate StringTemplate = Substitute.For<DataTemplate>();
		private static readonly DataTemplate DateTimeTemplate = Substitute.For<DataTemplate>();
		private static readonly DataTemplate CollectionTemplate = Substitute.For<DataTemplate>();
		private static readonly DataTemplate ObjectTemplate = Substitute.For<DataTemplate>();
		private static readonly DataTemplate NotSupportedTemplate = Substitute.For<DataTemplate>();

		private ProfilePropertyTemplateSelector profilePropertyTemplateSelector;

		[SetUp]
		public void SetUp()
		{
			profilePropertyTemplateSelector = new ProfilePropertyTemplateSelector
			                                  	{
			                                  		NumberTemplate = NumberTemplate,
			                                  		BooleanTemplate = BooleanTemplate,
			                                  		StringTemplate = StringTemplate,
			                                  		DateTimeTemplate = DateTimeTemplate,
			                                  		EnumerableTemplate = CollectionTemplate,
			                                  		ObjectTemplate = ObjectTemplate,
			                                  		NotSupportedTemplate = NotSupportedTemplate
			                                  	};
		}

		[TestCaseSource("SettingsProperties")]
		public void Should_return_correct_template(SettingsProperty settingsProperty, DataTemplate expectedTemplate)
		{
			var settingsPropertyValue = new SettingsPropertyValue(settingsProperty);
			var model = new ProfileProperty(settingsPropertyValue);

			var template = profilePropertyTemplateSelector.SelectTemplate(model, null);

			template.Should().BeSameAs(expectedTemplate);
		}

		public IEnumerable<ITestCaseData> SettingsProperties()
		{
			return NumberTypes()
				.Select(x => CreateTestData(x, NumberTemplate))
				.Union(SupportedEnumerableTypes().Select(x => CreateTestData(x, CollectionTemplate)))
				.Union(new []
				       	{
				       		CreateTestData(typeof(bool), BooleanTemplate),
							CreateTestData(typeof(string), StringTemplate),
							CreateTestData(typeof(DateTime), DateTimeTemplate),
							CreateTestData(typeof(object), NotSupportedTemplate),
							CreateTestData(typeof(IEnumerable), NotSupportedTemplate),
							CreateTestData(typeof(IEnumerable<int>), NotSupportedTemplate),
				       	});
		}

		private IEnumerable<Type> NumberTypes()
		{
			yield return typeof (sbyte);
			yield return typeof (byte);
			yield return typeof (short);
			yield return typeof (ushort);
			yield return typeof (int);
			yield return typeof (uint);
			yield return typeof (long);
			yield return typeof (ulong);
			yield return typeof (float);
			yield return typeof (double);
			yield return typeof (decimal);
		}

		private IEnumerable<Type> SupportedEnumerableTypes()
		{
			yield return (new int[0]).GetType();
		}

		private ITestCaseData CreateTestData(Type propertyType, DataTemplate expectedDataTemplate)
		{
			return new TestCaseData(CreateSettingsProperty(propertyType), expectedDataTemplate)
				.SetName("Property type '{0}'".Composite(propertyType))
				.SetDescription("Testing property type '{0}'".Composite(propertyType));
		}
		private SettingsProperty CreateSettingsProperty(Type propertyType)
		{
			var settingsProperty = new SettingsProperty("foo");
			settingsProperty.PropertyType = propertyType;

			return settingsProperty;
		}
	}
}