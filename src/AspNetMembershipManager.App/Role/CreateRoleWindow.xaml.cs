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
    public partial class CreateRoleWindow : EditDialogWindow
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