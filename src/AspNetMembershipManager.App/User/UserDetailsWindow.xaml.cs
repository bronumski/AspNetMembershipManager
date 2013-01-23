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
		
		private void CollectionPropertyEdit(object sender, RoutedEventArgs e)
		{
            var profileProperty = (ProfilePropertyViewModel)((Button)sender).DataContext;

			var propertyWindow = new ProfilePropertyWindow(this, profileProperty.PropertyName, profileProperty.PropertyType, profileProperty.PropertyValue);

			propertyWindow.ShowDialog();
		}
	}
}
