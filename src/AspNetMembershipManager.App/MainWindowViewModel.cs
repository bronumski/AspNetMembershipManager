﻿using System.Collections.Generic;
using AspNetMembershipManager.Web;

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

		public IEnumerable<IRole> Roles { get; private set; }

		public void RefreshMembershipUsers()
		{
			Users = providerManagers.GetAllUsers();

			OnPropertyChanged("Users");
		}

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