using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Profile;
using AspNetMembershipManager.Web.Profile;

namespace AspNetMembershipManager.Web.Security
{
	public class MembershipUser : IUser
	{
		private readonly System.Web.Security.MembershipUser membershipUser;
		private readonly IMembershipManager membershipManager;
		private readonly IRoleManager roleManager;
		private readonly IMapper<string, IRole> roleMapper;
		private readonly IDictionary<string, IRole> userRoles;
		private readonly ProfileBase profile;
		
		public MembershipUser(System.Web.Security.MembershipUser membershipUser, IMembershipManager membershipManager, IRoleManager roleManager, IProfileManager profileManager)
		{
			this.membershipUser = membershipUser;
			this.membershipManager = membershipManager;
			this.roleManager = roleManager;

			roleMapper = new RoleMapper(roleManager);
			if (roleManager.IsEnabled)
			{
				userRoles = roleMapper
					.MapAll(roleManager.GetRolesForUser(UserName))
					.ToDictionary(x => x.Name);
			}
			else
			{
				userRoles = new Dictionary<string, IRole>();
			}

			if (profileManager.IsEnabled)
			{
				profile = ProfileBase.Create(membershipUser.UserName);
			}
		}

		public string UserName
		{
			get { return membershipUser.UserName; }
		}

		public string EmailAddress
		{
			get { return membershipUser.Email; }
			set { membershipUser.Email = value; }
		}

		public IEnumerable<IRole> Roles
		{
			get { return userRoles.Values; }
		}

		public bool IsApproved
		{
			get { return membershipUser.IsApproved; }
		}

		public bool IsLockedOut
		{
			get { return membershipUser.IsLockedOut; }
		}

		public DateTime CreationDate
		{
			get { return membershipUser.CreationDate; }
		}

		public DateTime LastLoginDate
		{
			get { return membershipUser.LastLoginDate; }
		}

		public DateTime LastActivityDate
		{
			get { return membershipUser.LastActivityDate; }
		}

		public DateTime LastLockoutDate
		{
			get { return membershipUser.LastLockoutDate; }
		}

		public DateTime LastPasswordChangedDate
		{
			get { return membershipUser.LastPasswordChangedDate; }
		}

		public string Comment
		{
			get { return membershipUser.Comment; }
			set { membershipUser.Comment = value; }
		}

		public IEnumerable<SettingsPropertyValue> ProfileProperties
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
				                   select property
				               ).ToArray();
				    }
				}
				return Enumerable.Empty<SettingsPropertyValue>();
			}
		}

		public string PasswordQuestion
		{
			get { return membershipUser.PasswordQuestion; }
		}

		public void Delete()
		{
			membershipManager.DeleteUser(membershipUser.UserName);
		}

		public void Save()
		{
			membershipManager.UpdateUser(membershipUser);

			SaveUserRoles();

			SaveProfile();
		}

		private void SaveUserRoles()
		{
			if (roleManager.IsEnabled)
			{
				var currentRoles = roleManager.GetRolesForUser(UserName);

				var rolesToAdd = from newrole in Roles
				                 where !(from currentRole in currentRoles select currentRole).Contains(newrole.Name)
				                 select newrole;

				var rolesToRemove = from currentRole in currentRoles
				                    join newrole in Roles on currentRole equals newrole.Name into outer
				                    from o in outer.DefaultIfEmpty()
				                    where o == null
				                    select currentRole;

				foreach (var role in rolesToRemove)
				{
					roleManager.RemoveUserFromRole(UserName, role);
				}
				foreach (var role in rolesToAdd)
				{
					roleManager.AddUserToRole(UserName, role.Name);
				}
			}
		}

		private void SaveProfile()
		{
			if (profile != null)
			{
				if (profile.PropertyValues != null && profile.PropertyValues.Count > 0)
				{
					foreach (SettingsProvider prov in System.Web.Profile.ProfileManager.Providers)
					{
						var ppcv = new SettingsPropertyValueCollection();
						foreach (SettingsPropertyValue pp in profile.PropertyValues)
						{
							if (pp.Property.Provider.Name == prov.Name)
							{
								ppcv.Add(pp);
							}
						}
						if (ppcv.Count > 0)
						{
							prov.SetPropertyValues(profile.Context, ppcv);
						}
					}
					foreach (SettingsPropertyValue pp in profile.PropertyValues)
					{
						pp.IsDirty = false;
					}
				}
			}
		}

		public void RemoveFromRole(IRole role)
		{
			userRoles.Remove(role.Name);
		}

		public void AddToRole(IRole role)
		{
			userRoles.Add(role.Name, role);
		}

		public bool ChangePassword(string password)
		{
			return membershipUser.ChangePassword(membershipUser.ResetPassword(), password);
		}

		public bool ChangePasswordQuestionAndAnswer(string newPassword, string passwordQuestion, string passwordQuestionAnswer)
		{
			return membershipUser.ChangePasswordQuestionAndAnswer(newPassword, passwordQuestion, passwordQuestionAnswer);
		}

		public bool Unlock()
		{
			return membershipUser.UnlockUser();
		}
	}
}