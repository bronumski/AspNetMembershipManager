using System.Collections.Generic;
using System.Linq;
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

	        membershipUserMapper = new MembershipUserMapper(this.membershipManager, roleManager, profileManager);
	        roleMapper = new RoleMapper(this.roleManager);
		}

	    public IProfileManager ProfileManager { get; private set; }

		public IMembershipSettings MembershipSettings
		{
            get { return membershipManager.Settings; }
		}

		public bool RolesEnabled
        {
            get { return roleManager.IsEnabled; }
        }

	    public IEnumerable<IUser> GetAllUsers()
	    {
	        return membershipUserMapper.MapAll(membershipManager.GetAllUsers());
		}

		public IUser CreateUser(string username, string password, string emailAddress, string passwordQuestion, string passwordQuestionAnswer)
		{
			var status = membershipManager.CreateUser(username, password, emailAddress, passwordQuestion, passwordQuestionAnswer);

			if (status == MembershipCreateStatus.Success)
			{
				return membershipUserMapper.Map(membershipManager.GetUser(username));
			}
			throw new MembershipCreateUserException(status);
		}

	    public IEnumerable<IRole> GetAllRoles()
	    {
			if (RolesEnabled)
			{
				return roleMapper.MapAll(roleManager.GetAllRoles());
			}
	    	return Enumerable.Empty<IRole>();
	    }

	    public IRole CreateRole(string roleName)
	    {
	        roleManager.CreateRole(roleName);

            return roleMapper.Map(roleName);
	    }
	}
}