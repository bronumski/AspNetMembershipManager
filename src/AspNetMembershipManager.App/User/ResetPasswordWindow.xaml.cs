using System;
using System.Web.Security;
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
		private readonly IProviderManagers providerManagers;

		public ResetPasswordWindow(Window parentWindow, IUser user, IProviderManagers providerManagers)
			: base(parentWindow)
		{
			this.user = user;

			this.providerManagers = providerManagers;
			
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
			ResetPasswordModel.SetAutoGenerateneratedPassword(
				Membership.GeneratePassword(
					providerManagers.MembershipSettings.MinRequiredPasswordLength,
					providerManagers.MembershipSettings.MinRequiredNonAlphanumericCharacters));
		}

		protected override bool OnOkExecuted()
		{
			try
			{
				if (user.ChangePassword(ResetPasswordModel.NewPassword))
				{
					return base.OnOkExecuted();
				}
				ResetPasswordModel.Error = "Failed to change password";
			}
			catch (Exception ex)
			{
				ResetPasswordModel.Error = ex.Message;
			}
			return false;
		}
	}
}
