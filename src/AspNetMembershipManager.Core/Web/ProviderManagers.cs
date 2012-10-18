using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using AspNetMembershipManager.Web.Profile;
using AspNetMembershipManager.Web.Security;

namespace AspNetMembershipManager.Web
{
	public class ProviderManagers : IProviderManagers
	{
		public ProviderManagers(IMembershipManager membershipManager, IRoleManager roleManager, IProfileManager profileManager)
		{
			MembershipManager = membershipManager;
			RoleManager = roleManager;
			ProfileManager = profileManager;
		}

		public IMembershipManager MembershipManager { get; private set; }
		public IRoleManager RoleManager { get; private set; }
		public IProfileManager ProfileManager { get; private set; }

		public IEnumerable<IUser> GetAllUsers()
		{
			return MembershipManager.GetAllUsers().Select(x => new User(x, MembershipManager));
		}

		public IUser CreateUser(string username, string password, string emailAddress)
		{
			var status = MembershipManager.CreateUser(username, password, emailAddress);

			if (status == MembershipCreateStatus.Success)
			{
				return new User(MembershipManager.GetUser(username), MembershipManager);
			}
			throw new MembershipCreateUserException(status);
		}
	}
}