using System;
using System.Linq;

namespace AspNetMembershipManager.Web.Security
{
    public class Role : IRole, IEquatable<IRole>
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

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (Role)) return false;
			return Equals((IRole) obj);
		}

    	public bool Equals(IRole other)
    	{
    		return Name == other.Name;
    	}

    	public override int GetHashCode()
    	{
    		return (Name != null ? Name.GetHashCode() : 0);
    	}
    }
}