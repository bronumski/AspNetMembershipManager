using System;
using System.Windows;
using System.Windows.Controls;

namespace AspNetMembershipManager.User
{
	public class ProfilePropertyTemplateSelector : DataTemplateSelector
	{
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			var profileProperty = item as IProfileProperty;
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
			if (IsSupportedEnumerable(profileProperty))
			{
				return EnumerableTemplate;
			}
			return NotSupportedTemplate;
		}

		private bool IsSupportedEnumerable(IProfileProperty profileProperty)
		{
			return profileProperty.PropertyType.IsArray;
		}

		private static bool IsNumber(IProfileProperty value)
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
		public DataTemplate EnumerableTemplate { get; set; }
		public DataTemplate ObjectTemplate { get; set; }
		public DataTemplate NotSupportedTemplate { get; set; }
	}
}