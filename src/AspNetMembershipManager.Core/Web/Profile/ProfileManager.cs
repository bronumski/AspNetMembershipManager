using System.Web.Configuration;

namespace AspNetMembershipManager.Web.Profile
{
	public class ProfileManager : IProfileManager
    {
        private readonly ProfileSection profileSection;

        public ProfileManager(ProfileSection profileSection)
        {
            this.profileSection = profileSection;
        }

        public bool IsEnabled { get { return profileSection.Enabled; } }
    }
}