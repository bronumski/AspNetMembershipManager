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
    	private readonly IPasswordGenerator passwordGenerator;

        public CreateUserWindow(Window parentWindow, IProviderManagers providerManagers, IPasswordGenerator passwordGenerator)
			: base(parentWindow)
        {
            InitializeComponent();

            this.providerManagers = providerManagers;
        	this.passwordGenerator = passwordGenerator;

        	CreateUserModel = new CreateUserModel(providerManagers.MembershipSettings);
        }

		private CreateUserModel CreateUserModel
		{
			set { DataContext = value; }
            get { return (CreateUserModel)DataContext; }
		}

		protected override bool OnOkExecuted()
        {
        	try
        	{
				providerManagers.CreateUser(
                                            CreateUserModel.Username,
                                            CreateUserModel.Password,
                                            CreateUserModel.EmailAddress,
											CreateUserModel.PasswordQuestion,
											CreateUserModel.PasswordQuestionAnswer);
        	}
        	catch (MembershipCreateUserException ex)
        	{
                CreateUserModel.Error = ex.StatusCode.ToString();
        		return false;
        	}

        	return base.OnOkExecuted();
        }

    	private void AutoGeneratePassword_Click(object sender, RoutedEventArgs e)
    	{
    		CreateUserModel.SetAutoGenerateneratedPassword(passwordGenerator.GeneratePassword());
    	}
    }
}
