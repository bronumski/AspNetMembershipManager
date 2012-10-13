using System.Web.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AspNetMembershipManager.User
{
    /// <summary>
    /// Interaction logic for CreateUserWindow.xaml
    /// </summary>
    public partial class CreateUserWindow : Window
    {
        private readonly MembershipProvider membershipProvider;
        private readonly CreateUserModel createUserModel;

        public CreateUserWindow(MembershipProvider membershipProvider)
        {
            InitializeComponent();

            this.membershipProvider = membershipProvider;

            createUserModel = new CreateUserModel();
            DataContext = createUserModel;
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

        private void CreateUser_CanExecute(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = errors == 0;
			e.Handled = true;
		}

        private void CreateUser_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MembershipCreateStatus createStatus;
            membershipProvider.CreateUser(createUserModel.Username, txtPassword.Password,
                                          createUserModel.EmailAddress, null, null, true, null,
                                          out createStatus);

            if (createStatus == MembershipCreateStatus.Success)
            {
                DialogResult = e.Handled = true;
                Close();
            }
            else
            {
                createUserModel.Error = createStatus.ToString();
            }
        }
    }
}
