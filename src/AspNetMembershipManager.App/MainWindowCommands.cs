using System.Windows.Input;

namespace AspNetMembershipManager
{
	public static class MainWindowCommands
	{
		public static readonly RoutedUICommand ResetPassword = new RoutedUICommand("Reset password", "ResetPassword", typeof(MainWindow));
			 
	}
}