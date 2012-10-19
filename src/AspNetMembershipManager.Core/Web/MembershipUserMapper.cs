using AspNetMembershipManager.Web.Profile;
using AspNetMembershipManager.Web.Security;

namespace AspNetMembershipManager.Web
{
    class MembershipUserMapper : IMapper<System.Web.Security.MembershipUser, IUser>
    {
        private readonly IMembershipManager membershipManager;
    	private readonly IRoleManager roleManager;
    	private readonly IProfileManager profileManager;

    	public MembershipUserMapper(IMembershipManager membershipManager, IRoleManager roleManager, IProfileManager profileManager)
        {
        	this.membershipManager = membershipManager;
        	this.roleManager = roleManager;
    		this.profileManager = profileManager;
        }

    	public IUser Map(System.Web.Security.MembershipUser source)
        {
            return new MembershipUser(source, membershipManager, roleManager, profileManager);
        }
    }
}