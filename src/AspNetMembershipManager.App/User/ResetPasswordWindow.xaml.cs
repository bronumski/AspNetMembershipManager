using System;
using System.Windows;
using AspNetMembershipManager.Web;

namespace AspNetMembershipManager.User
{
	/// <summary>
	/// Interaction logic for ResetPasswordWindow.xaml
	/// </summary>
	public partial class ResetPasswordWindow : EditDialogWindow
	{
		private readonly IUser user;
		private readonly IPasswordGenerator passwordGenerator;

		public ResetPasswordWindow(Window parentWindow, IUser user, IProviderManagers providerManagers, IPasswordGenerator passwordGenerator)
			: base(parentWindow)
		{
			this.user = user;

			this.passwordGenerator = passwordGenerator;

			InitializeComponent();

			ResetPasswordModel = new ResetPasswordViewModel(providerManagers.MembershipSettings);
		}

		private ResetPasswordViewModel ResetPasswordModel
		{
			set { DataContext = value; }
            get { return (ResetPasswordViewModel)DataContext; }
		}

		private void AutoGeneratePassword_Click(object sender, RoutedEventArgs e)
		{
			ResetPasswordModel.SetAutoGenerateneratedPassword(passwordGenerator.GeneratePassword());
		}

		protected override bool OnOkExecuted()
		{
			try
			{
				if (user.ChangePassword(ResetPasswordModel.NewPassword))
				{
					if (ResetPasswordModel.ChangePasswordQuestionAndAnswer)
					{
						if (user.ChangePasswordQuestionAndAnswer(
							ResetPasswordModel.NewPassword,
							ResetPasswordModel.PasswordQuestion,
							ResetPasswordModel.PasswordQuestionAnswer))
						{
							return base.OnOkExecuted();
						}
						ResetPasswordModel.Error = "Failed to change password question and answer";
					}
					else
					{
						return base.OnOkExecuted();
					}
				}
				else
				{
					ResetPasswordModel.Error = "Failed to change password";
				}
				
			}
			catch (Exception ex)
			{
				ResetPasswordModel.Error = ex.Message;
			}
			return false;
		}
	}
}
