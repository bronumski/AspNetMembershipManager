using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetMembershipManager.Web.Security
{
	public class MembershipUser : IUser
	{
		private readonly System.Web.Security.MembershipUser membershipUser;
		private readonly IMembershipManager membershipManager;
		private readonly IRoleManager roleManager;
		private readonly IMapper<string, IRole> roleMapper;
		private readonly IDictionary<string, IRole> userRoles;
		
		public MembershipUser(System.Web.Security.MembershipUser membershipUser, IMembershipManager membershipManager, IRoleManager roleManager)
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

		public void Delete()
		{
			membershipManager.DeleteUser(membershipUser.UserName);
		}

		public void Save()
		{
			membershipManager.UpdateUser(membershipUser);

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

		public void RemoveFromRole(IRole role)
		{
			userRoles.Remove(role.Name);
		}

		public void AddToRole(IRole role)
		{
			userRoles.Add(role.Name, role);
		}
	}
}