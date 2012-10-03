using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Profile;
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

		public IEnumerable<ProfileProperties> Profile
		{
			get
			{
				var profile = ProfileBase.Create(Username);

				return (
					from SettingsProperty property in ProfileBase.Properties
					select new ProfileProperties(property, profile.GetPropertyValue(property.Name))
					).ToArray();
			}
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

		internal class ProfileProperties
		{
			private readonly object propertyValue;
			private readonly SettingsProperty property;

			public ProfileProperties(SettingsProperty property, object propertyValue)
			{
				this.property = property;
				this.propertyValue = propertyValue;
			}

			public string PropertyName
			{
				get { return property.Name; }
			}

			public Type PropertyType
			{
				get { return property.PropertyType; }
			}

			public object PropertyValue
			{
				get
				{
					if (propertyValue == null)
					{
						return "{null}";
					}
					if (propertyValue is IEnumerable)
					{
						var stringBuilder = new StringBuilder();
						
						stringBuilder.Append("[ ");
						
						foreach (var value in (IEnumerable)propertyValue)
						{
							stringBuilder.Append(value);
							stringBuilder.Append(", ");
						}
						
						stringBuilder.Append(" ]");

						return stringBuilder.ToString();
					}
					return propertyValue.ToString();
				}
			}
		}
    }
}