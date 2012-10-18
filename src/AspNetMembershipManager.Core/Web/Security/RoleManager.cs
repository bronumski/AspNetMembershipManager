using System.Collections.Generic;
using System.Web.Configuration;
using System.Web.Security;

namespace AspNetMembershipManager.Web.Security
{
	public class RoleManager : IRoleManager
	{
		private readonly RoleProvider roleProvider;
		private readonly RoleManagerSection roleSection;
		private readonly IMapper<string, RoleDetails> roleMapper;

		public RoleManager(RoleProvider roleProvider, RoleManagerSection roleSection)
		{
			this.roleProvider = roleProvider;
			this.roleSection = roleSection;
			roleMapper = new UserRoleRoleDetailsMapper(roleProvider);
		}

		public bool IsEnabled { get { return roleSection.Enabled; } }

		public bool DeleteRole(string name, bool throwOnPopulatedRole)
		{
			return roleProvider.DeleteRole(name, throwOnPopulatedRole);
		}

		public IEnumerable<RoleDetails> GetAllRoles()
		{
			return roleMapper.MapAll(roleProvider.GetAllRoles());
		}

		public void CreateRole(string roleName)
		{
			roleProvider.CreateRole(roleName);
		}

		public IEnumerable<string> GetRolesForUser(string userName)
		{
			return roleProvider.GetRolesForUser(userName);
		}

		public bool IsUserInRole(string username, string roleName)
		{
			return roleProvider.IsUserInRole(username, roleName);
		}

		public void AddUserToRole(string username, string roleName)
		{
			roleProvider.AddUserToRole(username, roleName);
		}

		public void RemoveUserFromRole(string username, string roleName)
		{
			roleProvider.RemoveUserFromRole(username, roleName);
		}
	}
}