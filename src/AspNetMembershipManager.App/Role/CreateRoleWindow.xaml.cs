using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AspNetMembershipManager.Web;
using AspNetMembershipManager.Web.Security;

namespace AspNetMembershipManager.Role
{
    /// <summary>
    /// Interaction logic for CreateRoleWindow.xaml
    /// </summary>
    public partial class CreateRoleWindow : Window
    {
        private readonly IRoleManager roleManager;
        private readonly CreateRoleModel createRoleModel;

        public CreateRoleWindow(Window parentWindow, IRoleManager roleManager)
        {
			Owner = parentWindow;

            this.roleManager = roleManager;
            InitializeComponent();

            createRoleModel = new CreateRoleModel();
            DataContext = createRoleModel;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            
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
				roleManager.CreateRole(createRoleModel.Name);
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