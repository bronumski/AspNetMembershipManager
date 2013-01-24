using System.Web.Security;
using System.Windows;
using System.Windows.Input;
using AspNetMembershipManager.Web;

namespace AspNetMembershipManager.User
{
    /// <summary>
    /// Interaction logic for CreateUserWindow.xaml
    /// </summary>
    public partial class CreateUserWindow : EditDialogWindow
    {
        private readonly IProviderManagers providerManagers;
        private readonly CreateUserModel createUserModel;

        public CreateUserWindow(Window parentWindow, IProviderManagers providerManagers)
        {
        	Owner = parentWindow;
            InitializeComponent();

            this.providerManagers = providerManagers;

            createUserModel = new CreateUserModel(providerManagers.MembershipSettings);
            DataContext = createUserModel;
        }

		

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
        	try
        	{
				providerManagers.CreateUser(
                                            createUserModel.Username,
                                            createUserModel.Password,
                                            createUserModel.EmailAddress,
											createUserModel.PasswordQuestion,
											createUserModel.PasswordQuestionAnswer);
                DialogResult = e.Handled = true;
                Close();
        	}
        	catch (MembershipCreateUserException ex)
        	{
        		createUserModel.Error = ex.StatusCode.ToString();
        	}
        }
    }
}
