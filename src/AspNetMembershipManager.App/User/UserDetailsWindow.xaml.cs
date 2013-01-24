using System;
using System.Windows;
using System.Windows.Controls;
using AspNetMembershipManager.User.Profile;
using AspNetMembershipManager.Web;

namespace AspNetMembershipManager.User
{
	/// <summary>
	/// Interaction logic for UserDetailsWindow.xaml
	/// </summary>
	partial class UserDetailsWindow : EditDialogWindow
	{
		private readonly UserDetailsModel userDetails;
		private readonly IUser user;

		internal UserDetailsWindow(Window parentWindow, IUser user, IProviderManagers providerManagers)
		{
	    	this.user = user;
			Owner = parentWindow;
			InitializeComponent();

            userDetails = new UserDetailsModel(user, providerManagers);

			DataContext = userDetails;
		}

        protected override bool OnOkExecuted()
        {
        	try
        	{
				user.Save();
        	}
        	catch (Exception ex)
        	{
                userDetails.Error = ex.Message;
        		return false;
        	}

        	return base.OnOkExecuted();
        }

        private void CustomProfilePropertyEdit(object sender, RoutedEventArgs e)
		{
            var profileProperty = (IProfileProperty)((Button)sender).DataContext;

			var propertyWindow = new ProfilePropertyWindow(this, profileProperty);

			propertyWindow.ShowDialog();
		}
		
		private void CollectionPropertyEdit(object sender, RoutedEventArgs e)
		{
            var profileProperty = (IProfileProperty)((Button)sender).DataContext;

			var propertyWindow = new ProfileCollectionPropertyWindow(this, profileProperty);

			propertyWindow.ShowDialog();
		}
	}
}
