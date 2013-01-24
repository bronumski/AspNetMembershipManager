using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AspNetMembershipManager
{
	public abstract class EditDialogWindow : Window
	{
		private int errors;

		protected EditDialogWindow()
		{
			ShowInTaskbar = false;
			WindowStartupLocation = WindowStartupLocation.CenterOwner;
		}

		protected void Validation_Error(object sender, ValidationErrorEventArgs e)
		{
			if (e.Action == ValidationErrorEventAction.Added)
			{
				AddError();
			}
			else
			{
				RemoveError();
			}
		}

		protected void AddError()
		{
			errors++;
		}

		protected void RemoveError()
		{
			errors--;
		}

        protected void Ok_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = errors == 0;
			e.Handled = true;
		}

        protected void Ok_Executed(object sender, ExecutedRoutedEventArgs e)
        {
        	if (!OnOkExecuted()) return;

        	DialogResult = true;
        	e.Handled = true;
        	Close();
        }

		protected virtual bool OnOkExecuted()
		{
			return true;
		}
	}
}
