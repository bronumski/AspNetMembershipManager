using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AspNetMembershipManager.User
{
	class ProfilePropertyTemplateSelector : DataTemplateSelector
	{
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			var profileProperty = item as ProfilePropertyViewModel;
			if (profileProperty == null)
			{
				return null;
			}
			if (IsNumber(profileProperty))
			{
				return NumberTemplate;
			}
			if (profileProperty.PropertyType == typeof(bool))
			{
				return BooleanTemplate;
			}
			if (profileProperty.PropertyType == typeof(string))
			{
				return StringTemplate;
			}
			if (profileProperty.PropertyType == typeof(DateTime))
			{
				return DateTimeTemplate;
			}
			if (IsCollection(profileProperty))
			{
				return CollectionTemplate;
			}
			return ObjectTemplate;
		}

		private bool IsCollection(ProfilePropertyViewModel profileProperty)
		{
			if (typeof(IEnumerable).IsAssignableFrom(profileProperty.PropertyType))
			{
				return true;
			}
			return profileProperty.PropertyType.GetInterface(typeof(IEnumerable<>).FullName) != null;
		}

		private static bool IsNumber(ProfilePropertyViewModel value)
		{
			if (value.PropertyType == typeof(sbyte)) return true;
			if (value.PropertyType == typeof(byte)) return true;
			if (value.PropertyType == typeof(short)) return true;
			if (value.PropertyType == typeof(ushort)) return true;
			if (value.PropertyType == typeof(int)) return true;
			if (value.PropertyType == typeof(uint)) return true;
			if (value.PropertyType == typeof(long)) return true;
			if (value.PropertyType == typeof(ulong)) return true;
			if (value.PropertyType == typeof(float)) return true;
			if (value.PropertyType == typeof(double)) return true;
			if (value.PropertyType == typeof(decimal)) return true;
			return false;
		}

		public DataTemplate NumberTemplate { get; set; }
		public DataTemplate StringTemplate { get; set; }
		public DataTemplate BooleanTemplate { get; set; }
		public DataTemplate DateTimeTemplate { get; set; }
		public DataTemplate CollectionTemplate { get; set; }
		public DataTemplate ObjectTemplate { get; set; }
	}
}