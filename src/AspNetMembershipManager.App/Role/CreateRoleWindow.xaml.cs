using System.Web.Security;
using System.Windows;

namespace AspNetMembershipManager.Role
{
    /// <summary>
    /// Interaction logic for CreateRoleWindow.xaml
    /// </summary>
    public partial class CreateRoleWindow : Window
    {
        private readonly RoleProvider roleProvider;
        private readonly CreateRoleModel createRoleModel;

        public CreateRoleWindow(RoleProvider roleProvider)
        {
            this.roleProvider = roleProvider;
            InitializeComponent();

            createRoleModel = new CreateRoleModel();
            DataContext = createRoleModel;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            roleProvider.CreateRole(createRoleModel.Name);
            DialogResult = true;
            Close();
        }
    }
}
