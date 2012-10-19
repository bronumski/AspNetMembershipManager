using System;
using System.Linq;
using System.Web.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AspNetMembershipManager.Initialization;
using AspNetMembershipManager.Role;
using AspNetMembershipManager.User;
using AspNetMembershipManager.Web;
using AspNetMembershipManager.Web.Security;

namespace AspNetMembershipManager
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly MainWindowViewModel viewModel;
	    private readonly IProviderManagers providerManagers;

        public MainWindow(IProviderManagers providerManagers)
        {
            InitializeComponent();

            this.providerManagers = providerManagers;

            viewModel = new MainWindowViewModel(providerManagers);

            try
            {
                DataContext = viewModel;

                RefreshMembers();
                RefreshRoles();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load Provider settings:\n" + ex, "Error loading providers...",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }

	    private void btnCreateUser_Click(object sender, RoutedEventArgs e)
        {
            var createUserDialog = new CreateUserWindow(this, providerManagers);
            var createResult = createUserDialog.ShowDialog();

            if (createResult == true)
            {
				RefreshMembers();
			}
        }

        private void btnCreateRole_Click(object sender, RoutedEventArgs e)
        {
            var createRoleDialog = new CreateRoleWindow(this, providerManagers);
            var createResult = createRoleDialog.ShowDialog();

            if (createResult == true)
            {
				RefreshRoles();
            }
        }
		
		private void DeleteRole(object sender, RoutedEventArgs e)
		{
			var role = (IRole)((Button) sender).DataContext;

			if (MessageBox.Show(this, "Delete role?", "Delete role", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
			{
			    role.Delete();
				
				RefreshRoles();
			}
		}

		private void DeleteUser(object sender, RoutedEventArgs e)
		{
			var user = (IUser)((Button) sender).DataContext;

			if (MessageBox.Show(this, "Delete user?", "Delete user", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
			{
				user.Delete();
				
				RefreshMembers();
			}
		}

		private void UserRowDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var row = (DataGridRow) sender;

			var user = (IUser) row.DataContext;

			var userDialog = new UserDetailsWindow(this, user, providerManagers);
            var refreshResult = userDialog.ShowDialog();

            if (refreshResult == true)
            {
            	RefreshMembers();
            	RefreshRoles();
            }
		}

		private void RefreshMembers()
		{
			viewModel.RefreshMembershipUsers();
		}

		private void RefreshRoles()
		{
			viewModel.RefreshRoles();
		}
	}
}
