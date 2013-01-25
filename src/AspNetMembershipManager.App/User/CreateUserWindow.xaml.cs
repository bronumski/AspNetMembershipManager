using System.Web.Security;
using System.Windows;
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
			: base(parentWindow)
        {
            InitializeComponent();

            this.providerManagers = providerManagers;

            createUserModel = new CreateUserModel(providerManagers.MembershipSettings);
            DataContext = createUserModel;
        }

		protected override bool OnOkExecuted()
        {
        	try
        	{
				providerManagers.CreateUser(
                                            createUserModel.Username,
                                            createUserModel.Password,
                                            createUserModel.EmailAddress,
											createUserModel.PasswordQuestion,
											createUserModel.PasswordQuestionAnswer);
        	}
        	catch (MembershipCreateUserException ex)
        	{
                createUserModel.Error = ex.StatusCode.ToString();
        		return false;
        	}

        	return base.OnOkExecuted();
        }
    }
}
