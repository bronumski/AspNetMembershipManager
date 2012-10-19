
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

		public MembershipUser(System.Web.Security.MembershipUser membershipUser, IMembershipManager membershipManager, IRoleManager roleManager)
		{
			this.membershipUser = membershipUser;
			this.membershipManager = membershipManager;
			this.roleManager = roleManager;

			roleMapper = new RoleMapper(roleManager);
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
			get
			{
				if (roleManager.IsEnabled)
				{
					return roleMapper.MapAll(roleManager.GetRolesForUser(UserName));
				}
				return Enumerable.Empty<IRole>();
			}
		}

		public void Delete()
		{
			membershipManager.DeleteUser(membershipUser.UserName);
		}

		public void Save()
		{
			membershipManager.UpdateUser(membershipUser);
		}
	}
}