using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AspNetMembershipManager.User.Profile
{
	/// <summary>
	/// Interaction logic for ProfilePropertyWindow.xaml
	/// </summary>
	partial class ProfilePropertyWindow : Window
	{


	    internal ProfilePropertyWindow(Window parentWindow, string propertyName, Type propertyType, Object propertyValue)
		{
	        Owner = parentWindow;

			InitializeComponent();

            ProfileProperty = new PropertyViewModel(propertyName, propertyType, propertyValue);
		}

        private PropertyViewModel ProfileProperty
		{
			set { DataContext = value; }
            get { return (PropertyViewModel)DataContext; }
		}
		
		private int errors;

		private void Validation_Error(object sender, ValidationErrorEventArgs e)
		{
			if (e.Action == ValidationErrorEventAction.Added)
			{
				errors++;
			}
			else
			{
				errors--;
			}
		}

        private void Ok_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = errors == 0;
			e.Handled = true;
		}

        private void Ok_Executed(object sender, ExecutedRoutedEventArgs e)
		{
			DialogResult = true;
			e.Handled = true;
			Close();
		}

	    private void Cancel_Click(object sender, RoutedEventArgs e)
	    {
	        
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
            Value = propertyValue ?? propertyType.GetConstructor(new Type[0]).Invoke(new Type[0]);
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