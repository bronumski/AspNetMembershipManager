using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using AspNetMembershipManager.Web;

namespace AspNetMembershipManager.User
{
	class UserDetailsModel : SaveViewModelBase
    {
		private readonly IUser user;
		private readonly IEnumerable<UserInRole> userRoles; 

		public UserDetailsModel(IUser user, IProviderManagers providerManagers)
		{
			this.user = user;

			if (providerManagers.RolesEnabled)
			{
				userRoles = providerManagers.GetAllRoles().Select(x => new UserInRole(x, user));
			}
		}

		public string Username
		{
			get { return user.UserName; }
		}

		public string EmailAddress
		{
			get { return user.EmailAddress; }
			set { user.EmailAddress = value; }
		}

		public IEnumerable<UserInRole> Roles
		{
			get { return userRoles; }
		}

		public IEnumerable<IProfileProperty> Profile
		{
			get
			{
				return user.ProfileProperties.Select(CreateProfilePropertyViewModel);
			}
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
                    case "Username":
                        if (string.IsNullOrEmpty(Username))
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