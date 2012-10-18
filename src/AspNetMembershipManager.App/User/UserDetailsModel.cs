using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Profile;
using AspNetMembershipManager.Web;
using AspNetMembershipManager.Web.Security;

namespace AspNetMembershipManager.User
{
	class UserDetailsModel : SaveViewModelBase
    {
		private readonly IUser user;
		private readonly ProfileBase profile;
		private readonly List<UserInRole> userRoles; 

		public UserDetailsModel(IUser user, IRoleManager roleManager, ProfileBase profile)
		{
			this.user = user;
			this.profile = profile;

            if (roleManager.IsEnabled)
			{
                userRoles = (
					from role in roleManager.GetAllRoles().Select(x => x.Name)
					join userInRole in roleManager.GetRolesForUser(user.UserName)
									on role equals userInRole into outer
					from o in outer.DefaultIfEmpty()
				select new UserInRole(role, o != null)).ToList();
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

		public IEnumerable<ProfilePropertyViewModel> Profile
		{
			get
			{
                if (profile != null)
                {
                    if (ProfileBase.Properties.Count > 0)
                    {
                        profile.GetPropertyValue(ProfileBase.Properties.Cast<SettingsProperty>().First().Name);

                        return (
                                   from SettingsPropertyValue property in profile.PropertyValues
                                   select new ProfilePropertyViewModel(property)
                               ).ToArray();
                    }
                }
			    return Enumerable.Empty<ProfilePropertyViewModel>();
			}
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
			public UserInRole(string roleName, bool isMember)
			{
				IsMember = isMember;
				RoleName = roleName;
			}

			public bool IsMember { get; set; }

			public string RoleName { get; private set; }
		}
    }
}