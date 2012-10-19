using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AspNetMembershipManager.Web;

namespace AspNetMembershipManager.Role
{
    /// <summary>
    /// Interaction logic for CreateRoleWindow.xaml
    /// </summary>
    public partial class CreateRoleWindow : Window
    {
        private readonly IProviderManagers providerManagers;
        private readonly CreateRoleModel createRoleModel;

        public CreateRoleWindow(Window parentWindow, IProviderManagers providerManagers)
        {
			Owner = parentWindow;

            this.providerManagers = providerManagers;
            InitializeComponent();

            createRoleModel = new CreateRoleModel();
            DataContext = createRoleModel;
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
				providerManagers.CreateRole(createRoleModel.Name);
				DialogResult = e.Handled = true;
				Close();
        	}
        	catch (Exception ex)
        	{
        		createRoleModel.Error = ex.Message;
        	}
        }
    }
}