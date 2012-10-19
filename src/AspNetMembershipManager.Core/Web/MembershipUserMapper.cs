using AspNetMembershipManager.Web.Security;

namespace AspNetMembershipManager.Web
{
    class MembershipUserMapper : IMapper<System.Web.Security.MembershipUser, IUser>
    {
        private readonly IMembershipManager membershipManager;

        public MembershipUserMapper(IMembershipManager membershipManager)
        {
            this.membershipManager = membershipManager;
        }

        public IUser Map(System.Web.Security.MembershipUser source)
        {
            return new MembershipUser(source, membershipManager);
        }
    }
}