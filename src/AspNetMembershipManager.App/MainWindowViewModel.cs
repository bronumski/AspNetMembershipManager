using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using AspNetMembershipManager.Web;
using AspNetMembershipManager.Web.Security;

namespace AspNetMembershipManager
{
	class MainWindowViewModel : ViewModelBase
	{
		private readonly IProviderManagers providerManagers;

		public MainWindowViewModel(IProviderManagers providerManagers)
		{
			this.providerManagers = providerManagers;
		}

		public IEnumerable<IUser> Users { get; private set; }

		public RoleDetails[] Roles { get; private set; }

		public void RefreshMembershipUsers()
		{
			Users = providerManagers.GetAllUsers();

			OnPropertyChanged("Users");
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