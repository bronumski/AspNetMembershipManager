using System.Linq;
using System.Web.Security;

namespace AspNetMembershipManager
{
	class MainWindowViewModel : ViewModelBase
	{
		private readonly IProviderManagers providerManagers;

		public MainWindowViewModel(IProviderManagers providerManagers)
		{
			this.providerManagers = providerManagers;
		}

		public MembershipUser[] MembershipUsers { get; private set; }

		public RoleDetails[] Roles { get; private set; }

		public void RefreshMembershipUsers()
		{
			MembershipUsers = providerManagers.MembershipManager.GetAllUsers().ToArray();

			OnPropertyChanged("MembershipUsers");
		}

		public bool RolesEnabled { get { return providerManagers.RoleManager.IsEnabled; }}

		public void RefreshRoles()
		{
			if (RolesEnabled)
			{
				Roles = providerManagers.RoleManager.GetAllRoles().ToArray();

				OnPropertyChanged("Roles");
			}
		}
	}
}