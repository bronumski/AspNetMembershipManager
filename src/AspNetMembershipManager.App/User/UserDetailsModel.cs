using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using AspNetMembershipManager.Web;

namespace AspNetMembershipManager.User
{
	class UserDetailsModel : SaveViewModelBase
    {
		public readonly IUser user;
		private readonly IProviderManagers providerManagers;

		public UserDetailsModel(IUser user, IProviderManagers providerManagers)
		{
			this.user = user;
			this.providerManagers = providerManagers;

			UnlockUserCommand = new RelayCommand<UserDetailsModel>(u => u != null && u.IsAccountLocked, u => u.UnlockAccount());
			ActivateUserCommand = new RelayCommand<UserDetailsModel>(u => u != null && ! u.IsAccountActivated, u => u.ActivatedAccount());
			DeactivateUserCommand = new RelayCommand<UserDetailsModel>(u => u != null && u.IsAccountActivated, u => u.DeactivateAccount());
		}

		public RelayCommand<UserDetailsModel> UnlockUserCommand { get; set; }

		public RelayCommand<UserDetailsModel> ActivateUserCommand { get; set; }

		public RelayCommand<UserDetailsModel> DeactivateUserCommand { get; set; }

		public string UserName
		{
			get { return user.UserName; }
		}

		public string EmailAddress
		{
			get { return user.EmailAddress; }
			set { user.EmailAddress = value; }
		}

		public bool RolesEnabled { get { return providerManagers.RolesEnabled; }}

		public bool ProfilesEnabled { get { return providerManagers.ProfilesEnabled; }}

		public IEnumerable<UserInRole> Roles
		{
			get
			{
				if (providerManagers.RolesEnabled)
				{
					return providerManagers.GetAllRoles().Select(x => new UserInRole(x, user));
				}
				return null;
			}
		}

		public IEnumerable<IProfileProperty> Profile
		{
			get { return user.ProfileProperties.Select(CreateProfilePropertyViewModel); }
		}

		public bool IsAccountLocked
		{
			get { return user.IsLockedOut; }
		}

		public bool IsAccountActivated
		{
			get { return user.IsApproved; }
		}

		public bool IsAccountDectivated
		{
			get { return ! user.IsApproved; }
		}

		public bool RequiresQuestionAndAnswer
    	{
    		get { return providerManagers.MembershipSettings.RequiresQuestionAndAnswer; }
    	}

		public string PasswordQuestion
		{
			get { return user.PasswordQuestion; }
		}

		public DateTime CreationDate
		{
			get { return user.CreationDate; }
		}

		public DateTime LastActivityDate
		{
			get { return user.LastActivityDate; }
		}

		public DateTime LastLockoutDate
		{
			get { return user.LastLockoutDate; }
		}

		public DateTime LastLoginDate
		{
			get { return user.LastLoginDate; }
		}

		public DateTime LastPasswordChangeDate
		{
			get { return user.LastPasswordChangedDate; }
		}

		public string Comment
		{ 
			get { return user.Comment; }
			set { user.Comment = value; }
		}

		private void UnlockAccount()
		{
			if (user.Unlock())
			{
				OnPropertyChanged("IsAccountLocked");

				UnlockUserCommand.UpdateCanExecuteState();
			}
		}

		private void ActivatedAccount()
		{
			user.Activate();

			AccountActiveStateChanged();
		}

		private void DeactivateAccount()
		{
			user.Deactivate();

			AccountActiveStateChanged();
		}

		private void AccountActiveStateChanged()
		{
			OnPropertyChanged("IsAccountActivated");
			OnPropertyChanged("IsAccountDectivated");
			ActivateUserCommand.UpdateCanExecuteState();
			DeactivateUserCommand.UpdateCanExecuteState();
		}

		private static IProfileProperty CreateProfilePropertyViewModel(SettingsPropertyValue x)
		{
			return new ProfileProperty(x);
		}

		public override string this[string columnName]
    	{
    		get
    		{
    			switch (columnName)
    			{
					case "EmailAddress":
						if (string.IsNullOrEmpty(EmailAddress) || EmailAddress.Length < 3)
						{
							return "Please enter a valid email address";
						}
						break;
                    case "UserName":
                        if (string.IsNullOrEmpty(UserName))
						{
							return "Please enter a unique username";
						}
						break;
    			}
    			return string.Empty;
    		}
    	}

		internal class UserInRole
		{
			private readonly IRole role;
			private readonly IUser user;

			public UserInRole(IRole role, IUser user)
			{
				this.role = role;
				this.user = user;
			}

			public bool IsMember
			{
				get { return user.Roles.Contains(role); }
				set
				{
					if (value)
					{
						user.AddToRole(role);
					}
					else
					{
						user.RemoveFromRole(role);
					}
				}
			}

			public string RoleName { get { return role.Name; } }
		}
    }
}