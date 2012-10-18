using System.Web.Security;

namespace AspNetMembershipManager
{
    class UserRoleRoleDetailsMapper : IMapper<string, RoleDetails>
    {
        private readonly RoleProvider roleProvider;

        public UserRoleRoleDetailsMapper(RoleProvider roleProvider)
        {
            this.roleProvider = roleProvider;
        }

        public RoleDetails Map(string source)
        {
            return new RoleDetails(source, roleProvider);
        }
    }
}