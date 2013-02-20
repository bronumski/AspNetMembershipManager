using System.Windows.Input;

namespace AspNetMembershipManager.User
{
	public static class UserDetailsWindowCommands
	{
		public static readonly RoutedUICommand ResetPassword = new RoutedUICommand("Reset password", "ResetPassword", typeof(UserDetailsWindow));
			 
	}
}