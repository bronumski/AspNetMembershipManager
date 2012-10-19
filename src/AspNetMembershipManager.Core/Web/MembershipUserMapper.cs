using AspNetMembershipManager.Web.Security;

namespace AspNetMembershipManager.Web
{
    class MembershipUserMapper : IMapper<System.Web.Security.MembershipUser, IUser>
    {
        private readonly IMembershipManager membershipManager;
    	private readonly IRoleManager roleManager;

    	public MembershipUserMapper(IMembershipManager membershipManager, IRoleManager roleManager)
        {
        	this.membershipManager = membershipManager;
        	this.roleManager = roleManager;
        }

    	public IUser Map(System.Web.Security.MembershipUser source)
        {
            return new MembershipUser(source, membershipManager, roleManager);
        }
    }
}