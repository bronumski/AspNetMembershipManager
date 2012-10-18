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
}