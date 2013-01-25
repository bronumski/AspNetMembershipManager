using System;
using System.Windows;
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
			: base(parentWindow)
        {
            this.providerManagers = providerManagers;
            InitializeComponent();

            createRoleModel = new CreateRoleModel();
            DataContext = createRoleModel;
        }

		protected override bool OnOkExecuted()
        {
        	try
        	{
				providerManagers.CreateRole(createRoleModel.Name);
        	}
        	catch (Exception ex)
        	{
        		createRoleModel.Error = ex.Message;
        		return false;
        	}
			return base.OnOkExecuted();
        }
    }
}