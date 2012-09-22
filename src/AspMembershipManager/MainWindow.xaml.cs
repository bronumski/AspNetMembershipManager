using System;
using System.Linq;
using System.Web.Security;
using System.Windows;
using AspMembershipManager.Initialization;
using AspMembershipManager.Role;
using AspMembershipManager.User;

namespace AspMembershipManager
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly MainWindowViewModel viewModel;
	    private readonly Providers providers;

		public MainWindow()
		{
			InitializeComponent();


			var initializeDialog = new InitializationWindow();
			var initializationResult = initializeDialog.ShowDialog();

			if (initializationResult != true)
			{
				Application.Current.Shutdown();
			}
			else
			{
                viewModel = new MainWindowViewModel();

			    providers = initializeDialog.Providers;
				try
				{
					DataContext = viewModel;
                    int totalRecords = 0;
                    viewModel.RefreshMembershipUsers(providers.MembershipProvider.GetAllUsers(0, int.MaxValue, out totalRecords).Cast<MembershipUser>());
                    viewModel.RefreshRoles(new UserRoleRoleDetailsMapper(providers.RoleProvider).MapAll(providers.RoleProvider.GetAllRoles()));
				}
				catch(Exception ex)
				{
					MessageBox.Show("Failed to load Provider settings:\n" + ex, "Error loading providers...", MessageBoxButton.OK,
					                MessageBoxImage.Error);
					Application.Current.Shutdown();
				}
			}
		}

        private void btnCreateUser_Click(object sender, RoutedEventArgs e)
        {
            var createUserDialog = new CreateUserWindow(providers.MembershipProvider);
            var createResult = createUserDialog.ShowDialog();

            if (createResult == true)
            {
                int totalRecords = 0;
                viewModel.RefreshMembershipUsers(providers.MembershipProvider.GetAllUsers(0, int.MaxValue, out totalRecords).Cast<MembershipUser>());
            }
        }

        private void btnCreateRole_Click(object sender, RoutedEventArgs e)
        {
            var createRoleDialog = new CreateRoleWindow(providers.RoleProvider);
            var createResult = createRoleDialog.ShowDialog();

            if (createResult == true)
            {
                viewModel.RefreshRoles(new UserRoleRoleDetailsMapper(providers.RoleProvider).MapAll(providers.RoleProvider.GetAllRoles()));
            }
        }
	}
}
