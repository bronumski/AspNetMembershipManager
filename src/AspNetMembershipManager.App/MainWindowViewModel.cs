using System.Collections.Generic;
using System.Linq;
using AspNetMembershipManager.User;
using AspNetMembershipManager.Web;

namespace AspNetMembershipManager
{
	class MainWindowViewModel : ViewModelBase
	{
		private readonly IProviderManagers providerManagers;

		public MainWindowViewModel(IProviderManagers providerManagers)
		{
			this.providerManagers = providerManagers;

			//UnlockUserCommand = new RelayCommand<UserDetailsModel>(user => user != null && user.IsAccountLocked, user => { user.Unlock(); RefreshMembershipUsers(); });
			//ActivateUserCommand = new RelayCommand<UserDetailsModel>(user => user != null && user.IsAccountDectivated, user => { user.Activate(); RefreshMembershipUsers(); });
			//DeactivateUserCommand = new RelayCommand<UserDetailsModel>(user => user != null && user.Is, user => { user.Deactivate(); RefreshMembershipUsers(); });
		}

		//public RelayCommand<IUser> UnlockUserCommand { get; set; }

		//public RelayCommand<IUser> ActivateUserCommand { get; set; }

		//public RelayCommand<IUser> DeactivateUserCommand { get; set; }

		private UserDetailsModel selectedUser;
		public UserDetailsModel SelectedUser
		{
			get { return selectedUser; }
			set
			{
				selectedUser = value;

				//UnlockUserCommand.UpdateCanExecuteState();
				//ActivateUserCommand.UpdateCanExecuteState();
				//DeactivateUserCommand.UpdateCanExecuteState();

				OnPropertyChanged("SelectedUser");
			}
		}

		public IRole SelectedRole { get; set; }

		public IEnumerable<UserDetailsModel> Users { get; private set; }

		public void RefreshMembershipUsers()
		{
			Users = providerManagers.GetAllUsers().Select(x => new UserDetailsModel(x, providerManagers));

			OnPropertyChanged("Users");
		}

		public IEnumerable<IRole> Roles { get; private set; }

		public bool RolesEnabled { get { return providerManagers.RolesEnabled; }}

		public void RefreshRoles()
		{
			if (RolesEnabled)
			{
				Roles = providerManagers.GetAllRoles();

				OnPropertyChanged("Roles");
			}
		}
	}
}