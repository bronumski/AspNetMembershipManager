using System.Linq;
using System.Web.Security;

namespace AspNetMembershipManager
{
    public class RoleDetails
    {
        private readonly RoleProvider roleProvider;

        public RoleDetails(string name, RoleProvider roleProvider)
        {
            this.roleProvider = roleProvider;
            Name = name;
        }

        public string Name { get; private set; }

        public int UsersInRole
        {
            get { return roleProvider.GetUsersInRole(Name).Count(); }
        }
    }
}