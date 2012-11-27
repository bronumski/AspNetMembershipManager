using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AspNetMembershipManager.User.Profile;
using AspNetMembershipManager.Web;

namespace AspNetMembershipManager.User
{
	/// <summary>
	/// Interaction logic for UserDetailsWindow.xaml
	/// </summary>
	partial class UserDetailsWindow : Window
	{
		private readonly UserDetailsModel userDetails;
		private int errors;
		private readonly IUser user;

		internal UserDetailsWindow(Window parentWindow, IUser user, IProviderManagers providerManagers)
		{
	    	this.user = user;
			Owner = parentWindow;
			InitializeComponent();

            userDetails = new UserDetailsModel(user, providerManagers);

			DataContext = userDetails;
		}

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

        private void SaveUser_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = errors == 0;
			e.Handled = true;
		}

        private void SaveUser_Executed(object sender, ExecutedRoutedEventArgs e)
        {
        	try
        	{
				user.Save();

        		DialogResult = e.Handled = true;
                Close();
        	}
        	catch (Exception ex)
        	{
                userDetails.Error = ex.Message;
        	}
        }

        private void CustomProfilePropertyEdit(object sender, RoutedEventArgs e)
		{
            var profileProperty = (ProfilePropertyViewModel)((Button)sender).DataContext;

			var propertyWindow = new ProfilePropertyWindow(this, profileProperty.PropertyName, profileProperty.PropertyType, profileProperty.PropertyValue);

			propertyWindow.ShowDialog();
		}
	}

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
            return ObjectTemplate;
        }

        public static bool IsNumber(ProfilePropertyViewModel value)
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
        public DataTemplate ObjectTemplate { get; set; }
    }
}
