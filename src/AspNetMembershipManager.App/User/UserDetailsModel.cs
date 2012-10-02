using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Security;

namespace AspNetMembershipManager.User
{
	class UserDetailsModel: INotifyPropertyChanged
    {
		private readonly MembershipUser user;
		private readonly List<UserInRole> userRoles; 

		public UserDetailsModel(MembershipUser user, RoleProvider roleProvider)
		{
			this.user = user;

			userRoles = (
					from role in roleProvider.GetAllRoles()
					join userInRole in roleProvider.GetRolesForUser(user.UserName)
									on role equals userInRole into outer
					from o in outer.DefaultIfEmpty()
				select new UserInRole(role, o != null)).ToList();
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public string Username
		{
			get { return user.UserName; }
		}

		public string EmailAddress
		{
			get { return user.Email; }
			set { user.Email = value; }
		}

		public IEnumerable<UserInRole> Roles
		{
			get { return userRoles; }
		}

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
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