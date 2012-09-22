using System.Web.Security;
using System.Windows;

namespace AspMembershipManager.User
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
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
