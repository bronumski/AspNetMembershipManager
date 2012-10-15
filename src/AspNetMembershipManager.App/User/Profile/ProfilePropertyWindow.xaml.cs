using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AspNetMembershipManager.User.Profile
{
	/// <summary>
	/// Interaction logic for ProfilePropertyWindow.xaml
	/// </summary>
	partial class ProfilePropertyWindow : Window
	{
		internal ProfilePropertyWindow(Window parentWindow, ProfilePropertyViewModel profileProperty)
		{
			Owner = parentWindow;

			InitializeComponent();

			ProfileProperty = profileProperty;
		}

		private ProfilePropertyViewModel ProfileProperty
		{
			set { DataContext = value; }
			get { return (ProfilePropertyViewModel) DataContext; }
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
			DialogResult = true;
			e.Handled = true;
			ProfileProperty.SetPropertyValue();
			Close();
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			ProfileProperty.ResetPropertyValue();
		}
	}
}
