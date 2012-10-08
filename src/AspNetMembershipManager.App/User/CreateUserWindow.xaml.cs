using System.Web.Security;
using System.Windows;

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

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            MembershipCreateStatus createStatus;
            membershipProvider.CreateUser(createUserModel.Username, createUserModel.Password,
                                          createUserModel.EmailAddress, null, null, true, null,
                                          out createStatus);

            DialogResult = createStatus == MembershipCreateStatus.Success;
            Close();
        }
    }
}
