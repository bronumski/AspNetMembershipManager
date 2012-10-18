using System;
using System.Web.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AspNetMembershipManager.Web;
using AspNetMembershipManager.Web.Security;

namespace AspNetMembershipManager.User
{
    /// <summary>
    /// Interaction logic for CreateUserWindow.xaml
    /// </summary>
    public partial class CreateUserWindow : Window
    {
        private readonly IProviderManagers providerManagers;
        private readonly CreateUserModel createUserModel;

        public CreateUserWindow(Window parentWindow, IProviderManagers providerManagers)
        {
        	Owner = parentWindow;
            InitializeComponent();

            this.providerManagers = providerManagers;

            createUserModel = new CreateUserModel();
            DataContext = createUserModel;
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

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = errors == 0;
			e.Handled = true;
		}

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
        	try
        	{
				IUser user = providerManagers.CreateUser(
                                            createUserModel.Username,
                                            txtPassword.Password,
                                            createUserModel.EmailAddress);
                DialogResult = e.Handled = true;
                Close();
        	}
        	catch (MembershipCreateUserException ex)
        	{
        		createUserModel.Error = ex.StatusCode.ToString();
        	}
        }
    }
}
