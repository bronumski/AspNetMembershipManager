using System.Linq;

namespace AspNetMembershipManager.Web.Security
{
    public class Role : IRole
    {
        private readonly IRoleManager roleManager;

        public Role(string name, IRoleManager roleManager)
        {
            this.roleManager = roleManager;
            Name = name;
        }

        public string Name { get; private set; }

        public int UsersInRole
        {
            get { return roleManager.GetUsersInRole(Name).Count(); }
        }

        public void Delete()
        {
        	roleManager.DeleteRole(Name);
        }
    }
}