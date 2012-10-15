using System.Collections.Generic;
using System.Linq;
using System.Web.Security;

namespace AspNetMembershipManager
{
	class MainWindowViewModel : ViewModelBase
	{

		public MembershipUser[] MembershipUsers { get; private set; }

		public RoleDetails[] Roles { get; private set; }

		public void RefreshMembershipUsers(IEnumerable<MembershipUser> membershipUsers)
		{
			MembershipUsers = membershipUsers.ToArray();

			OnPropertyChanged("MembershipUsers");
		}

		public void RefreshRoles(IEnumerable<RoleDetails> roles)
		{
			Roles = roles.ToArray();

			OnPropertyChanged("Roles");
		}
	}

	class RoleDetails
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