using System;
using System.Collections.Generic;
using System.Windows.Input;
using AspNetMembershipManager.Web;

namespace AspNetMembershipManager
{
	class MainWindowViewModel : ViewModelBase
	{
		private readonly IProviderManagers providerManagers;

		public MainWindowViewModel(IProviderManagers providerManagers)
		{
			this.providerManagers = providerManagers;

			UnlockUserCommand = new RelayCommand<IUser>(user => user != null && user.IsLockedOut, user => { user.Unlock(); RefreshMembershipUsers(); });
			ActivateUserCommand = new RelayCommand<IUser>(user => user != null && ! user.IsApproved, user => { user.Activate(); RefreshMembershipUsers(); });
			DeactivateUserCommand = new RelayCommand<IUser>(user => user != null && user.IsApproved, user => { user.Deactivate(); RefreshMembershipUsers(); });
		}

		public RelayCommand<IUser> UnlockUserCommand { get; set; }

		public RelayCommand<IUser> ActivateUserCommand { get; set; }

		public RelayCommand<IUser> DeactivateUserCommand { get; set; }

		private IUser selectedUser;
		public IUser SelectedUser
		{
			get { return selectedUser; }
			set
			{
				selectedUser = value;

				UnlockUserCommand.UpdateCanExecuteState();
				ActivateUserCommand.UpdateCanExecuteState();
				DeactivateUserCommand.UpdateCanExecuteState();

				OnPropertyChanged("SelectedUser");
			}
		}

		public IRole SelectedRole { get; set; }

		public IEnumerable<IUser> Users { get; private set; }

		public void RefreshMembershipUsers()
		{
			Users = providerManagers.GetAllUsers();

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