using System.Web.Configuration;
using System.Web.Profile;

namespace AspNetMembershipManager.Web.Profile
{
	public class ProfileManager : IProfileManager
    {
        private readonly ProfileSection profileSection;

        public ProfileManager(ProfileProvider profileProvider, ProfileSection profileSection)
        {
            this.profileSection = profileSection;
        }

        public bool IsEnabled { get { return profileSection.Enabled; } }
    }
}