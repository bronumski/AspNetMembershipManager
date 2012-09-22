using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AspMembershipManager.Role
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
