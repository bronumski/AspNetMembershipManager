using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AspNetMembershipManager.Role;
using AspNetMembershipManager.User;
using AspNetMembershipManager.Web;

namespace AspNetMembershipManager
{
	public static class MainWindowCommands
	{
		public static readonly RoutedUICommand ResetPassword = new RoutedUICommand("Reset password", "ResetPassword", typeof(MainWindow));
			 
	}
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

	    private void CreateUserExecuted(object sender, RoutedEventArgs e)
        {
            var createUserDialog = new CreateUserWindow(this, providerManagers, new MembershipPasswordGenerator(providerManagers.MembershipSettings));
            var createResult = createUserDialog.ShowDialog();

            if (createResult == true)
            {
				RefreshMembers();
			}
        }

		private void DeleteUserExecuted(object sender, RoutedEventArgs e)
		{
			var user = (UserDetailsModel)((DataGrid) sender).SelectedItem;

			if (MessageBox.Show(this, "Delete user?", "Delete user", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
			{
				//TODO: Change!
				user.user.Delete();
				RefreshMembers();
			}
		}

		private void UserRowDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var row = (DataGridRow) sender;

			var user = (UserDetailsModel) row.DataContext;

			var userDialog = new UserDetailsWindow(this, user, providerManagers);
			var refreshResult = userDialog.ShowDialog();

			if (refreshResult == true)
			{
				RefreshMembers();
				RefreshRoles();
			}
		}

		private void ResetPasswordExecuted(object sender, RoutedEventArgs e)
		{
			var user = (UserDetailsModel)((DataGrid) sender).SelectedItem;

			var resetPasswordDialog = new ResetPasswordWindow(this, /*TODO: Change*/ user.user, providerManagers, new MembershipPasswordGenerator(providerManagers.MembershipSettings));
			resetPasswordDialog.ShowDialog();
		}

		private void CreateRoleExecuted(object sender, RoutedEventArgs e)
        {
        	var createRoleDialog = new CreateRoleWindow(this, providerManagers);
			var createResult = createRoleDialog.ShowDialog();

			if (createResult == true)
			{
				RefreshRoles();
			}
        }

		private void DeleteRoleExecuted(object sender, RoutedEventArgs e)
		{
			var role = (IRole)((DataGrid) sender).SelectedItem;

			if (MessageBox.Show(this, "Delete role?", "Delete role", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
			{
			    role.Delete();
				
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
