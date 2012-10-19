using AspNetMembershipManager.Web.Security;

namespace AspNetMembershipManager.Web
{
    class RoleMapper : IMapper<string, IRole>
    {
        private readonly IRoleManager roleManager;

        public RoleMapper(IRoleManager roleManager)
        {
            this.roleManager = roleManager;
        }

        public IRole Map(string source)
        {
            return new Role(source, roleManager);
        }
    }
}