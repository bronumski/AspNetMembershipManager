using System;
using System.Configuration;
using System.Web.Profile;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AspNetMembershipManager.User.Profile;
using AspNetMembershipManager.Web;
using AspNetMembershipManager.Web.Profile;

namespace AspNetMembershipManager.User
{
	/// <summary>
	/// Interaction logic for UserDetailsWindow.xaml
	/// </summary>
	partial class UserDetailsWindow : Window
	{
		private readonly UserDetailsModel userDetails;
	    private readonly IProfileManager profileManager;
		private int errors;
		private readonly IUser user;
		private ProfileBase profileBase;

		internal UserDetailsWindow(Window parentWindow, IUser user, IProviderManagers providerManagers)
		{
	    	this.user = user;
			Owner = parentWindow;
			InitializeComponent();

            profileManager = providerManagers.ProfileManager;
		    profileBase = null;
            if (profileManager.IsEnabled)
            {
                profileBase = ProfileBase.Create(user.UserName);
            }
            userDetails = new UserDetailsModel(user, providerManagers, profileBase);

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
                //Transaction scope does not work with SqlCE in this context
                //"The connection object can not be enlisted in transaction scope."
				//using (var transactionScope = new TransactionScope())
				{
					user.Save();

                    if (System.Web.Profile.ProfileManager.Enabled)
                    {
                        if (profileBase.PropertyValues != null && profileBase.PropertyValues.Count > 0)
                        {
                            foreach (SettingsProvider prov in System.Web.Profile.ProfileManager.Providers)
                            {
                                var ppcv = new SettingsPropertyValueCollection();
                                foreach (SettingsPropertyValue pp in profileBase.PropertyValues)
                                {
                                    if (pp.Property.Provider.Name == prov.Name)
                                    {
                                        ppcv.Add(pp);
                                    }
                                }
                                if (ppcv.Count > 0)
                                {
                                    prov.SetPropertyValues(profileBase.Context, ppcv);
                                }
                            }
                            foreach (SettingsPropertyValue pp in profileBase.PropertyValues)
                            {
                                pp.IsDirty = false;
                            }
                        }
                    }
				    //transactionScope.Complete();
				}

        		DialogResult = e.Handled = true;
                Close();
        	}
        	catch (Exception ex)
        	{
                userDetails.Error = ex.Message;
        	}
        }

		private void ProfilePropertyDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var row = (DataGridRow) sender;

			var profileProperty = (ProfilePropertyViewModel) row.DataContext;

			var propertyWindow = new ProfilePropertyWindow(this, profileProperty);

			propertyWindow.ShowDialog();
		}
	}
}
