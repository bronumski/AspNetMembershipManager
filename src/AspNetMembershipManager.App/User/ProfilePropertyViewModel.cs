using System;
using System.ComponentModel;
using System.Configuration;

namespace AspNetMembershipManager.User
{
	class ProfilePropertyViewModel : SaveViewModelBase
	{
		private readonly SettingsPropertyValue property;
		private readonly TypeConverter converter;
		private readonly object originalValue;

		public ProfilePropertyViewModel(SettingsPropertyValue property)
		{
			this.property = property;

			converter = TypeDescriptor.GetConverter(PropertyType);
			try
			{
				originalValue = property.PropertyValue;
			}
			catch (Exception) {}
		}

		public string PropertyName
		{
			get { return property.Name; }
		}

		public Type PropertyType
		{
			get { return property.Property.PropertyType; }
		}

		public object PropertyValue
		{
			get
			{
                return property.PropertyValue;
			}
			set
			{
                property.PropertyValue = value;
				OnPropertyChanged("PropertyValue");
			}
		}

		public void ResetPropertyValue()
		{
			property.PropertyValue = originalValue;
			OnPropertyChanged("PropertyValue");
		}

		public override string this[string columnName]
		{
			get
			{ 
    			switch (columnName)
    			{
					case "PropertyValue":
						if (!converter.IsValid(PropertyValue))
						{
							return string.Format("Property value is not a valid '{0}'", PropertyType);
						}
						break;
    			}
    			return string.Empty;
			}
		}
	}
}