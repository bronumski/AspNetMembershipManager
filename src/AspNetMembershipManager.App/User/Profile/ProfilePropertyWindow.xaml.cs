using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace AspNetMembershipManager.User.Profile
{
	/// <summary>
	/// Interaction logic for ProfilePropertyWindow.xaml
	/// </summary>
	partial class ProfilePropertyWindow : EditDialogWindow
	{
	    internal ProfilePropertyWindow(Window parentWindow, IProfileProperty profileProperty)
			: base(parentWindow)
		{
			InitializeComponent();

            ProfileProperty = new PropertyViewModel(profileProperty.PropertyName, profileProperty.PropertyType, profileProperty.PropertyValue);
		}

        private PropertyViewModel ProfileProperty
		{
			set { DataContext = value; }
            get { return (PropertyViewModel)DataContext; }
		}

		private void CustomProfilePropertyEdit(object sender, RoutedEventArgs e)
	    {
	        throw new NotImplementedException();
	    }
	}

    class PropertyViewModel
    {
        public PropertyViewModel(string propertyName, Type propertyType, Object propertyValue)
        {
            Name = propertyName;
            Type = propertyType;
            Value = propertyValue ?? CreateInstance(propertyType);
        }

    	private static object CreateInstance(Type propertyType)
    	{
			if (propertyType.IsArray)
			{
				return propertyType.GetConstructor(new[] { typeof(int) } ).Invoke(new object[] { 0 });
			}
    		return propertyType.GetConstructor(new Type[0]).Invoke(new Type[0]);
    	}

    	public string Name { get; private set; }

        public Type Type { get; private set; }

        public object Value { get; private set; }

        public IEnumerable<PropertyViewModel> Properties
        {
            get
            {
                foreach (var propertyInfo in Type.GetProperties())
                {
                    yield return new PropertyViewModel(propertyInfo.Name, propertyInfo.PropertyType, propertyInfo.GetValue(Value, null));
                }
            }
        }
    }
}