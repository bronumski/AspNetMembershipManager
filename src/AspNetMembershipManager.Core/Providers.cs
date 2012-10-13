using System.Web.Profile;
using System.Web.Security;

namespace AspNetMembershipManager
{
    public class Providers
    {
        public Providers(MembershipProvider membershipProvider, RoleProvider roleProvider, ProfileProvider profileProvider)
        {
            MembershipProvider = membershipProvider;
            RoleProvider = roleProvider;
        	ProfileProvider = profileProvider;
        }

        public MembershipProvider MembershipProvider { get; private set; }
        public RoleProvider RoleProvider { get; private set; }
    	public ProfileProvider ProfileProvider { get; set; }
    }
}