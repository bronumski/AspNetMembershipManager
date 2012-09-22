using System.Web.Security;

namespace AspNetMembershipManager
{
    public class Providers
    {
        public Providers(MembershipProvider membershipProvider, RoleProvider roleProvider)
        {
            MembershipProvider = membershipProvider;
            RoleProvider = roleProvider;
        }

        public MembershipProvider MembershipProvider { get; private set; }
        public RoleProvider RoleProvider { get; private set; }
    }
}