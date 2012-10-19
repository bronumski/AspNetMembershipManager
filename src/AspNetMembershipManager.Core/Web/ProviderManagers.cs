using System.Collections.Generic;
using System.Web.Security;
using AspNetMembershipManager.Web.Profile;
using AspNetMembershipManager.Web.Security;

namespace AspNetMembershipManager.Web
{
	public class ProviderManagers : IProviderManagers
	{
	    private readonly IMembershipManager membershipManager;
	    private readonly IRoleManager roleManager;
	    private readonly IMapper<string, IRole> roleMapper;
	    private readonly IMapper<System.Web.Security.MembershipUser, IUser> membershipUserMapper;

	    public ProviderManagers(IMembershipManager membershipManager, IRoleManager roleManager, IProfileManager profileManager)
		{
			this.membershipManager = membershipManager;
			this.roleManager = roleManager;
			ProfileManager = profileManager;

	        membershipUserMapper = new MembershipUserMapper(this.membershipManager);
	        roleMapper = new RoleMapper(this.roleManager);
		}

	    public IProfileManager ProfileManager { get; private set; }

	    public bool RolesEnabled
        {
            get { return roleManager.IsEnabled; }
        }

	    public IEnumerable<IUser> GetAllUsers()
	    {
	        return membershipUserMapper.MapAll(membershipManager.GetAllUsers());
		}

		public IUser CreateUser(string username, string password, string emailAddress)
		{
			var status = membershipManager.CreateUser(username, password, emailAddress);

			if (status == MembershipCreateStatus.Success)
			{
				return membershipUserMapper.Map(membershipManager.GetUser(username));
			}
			throw new MembershipCreateUserException(status);
		}

	    public IEnumerable<IRole> GetAllRoles()
	    {
	        return roleMapper.MapAll(roleManager.GetAllRoles());
	    }

	    public IRole CreateRole(string roleName)
	    {
	        roleManager.CreateRole(roleName);

            return roleMapper.Map(roleName);
	    }
	}
}