using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.Profile;
using System.Web.Security;
using AspNetMembershipManager.Web.Security;

namespace AspNetMembershipManager
{
    public interface IProviderManagers
    {
        IMembershipManager MembershipManager { get; }
        IRoleManager RoleManager { get; }
        IProfileManager ProfileManager { get; set; }
    }

    public class ProviderManagers : IProviderManagers
    {
        public ProviderManagers(IMembershipManager membershipProvider, IRoleManager roleProvider, IProfileManager profileProvider)
        {
            MembershipManager = membershipProvider;
            RoleManager = roleProvider;
            ProfileManager = profileProvider;
        }

        public IMembershipManager MembershipManager { get; private set; }
        public IRoleManager RoleManager { get; private set; }
    	public IProfileManager ProfileManager { get; set; }
    }

    public interface IMembershipManager
    {
        bool DeleteUser(string userName, bool deleteAllRelatedData);
        IEnumerable<MembershipUser> GetAllUsers();
        void UpdateUser(MembershipUser user);
        MembershipCreateStatus CreateUser(string username, string password, string emailAddress);
    }

    public class MembershipManager : IMembershipManager
    {
        private readonly MembershipProvider membershipProvider;

        public MembershipManager(MembershipProvider membershipProvider, MembershipSection membershipSection)
        {
            this.membershipProvider = membershipProvider;
        }

        public bool DeleteUser(string userName, bool deleteAllRelatedData)
        {
            return membershipProvider.DeleteUser(userName, deleteAllRelatedData);
        }

        public IEnumerable<MembershipUser> GetAllUsers()
        {
            int totalRecords;
            return membershipProvider.GetAllUsers(0, int.MaxValue, out totalRecords).Cast<MembershipUser>();
        }

        public void UpdateUser(MembershipUser user)
        {
            membershipProvider.UpdateUser(user);
        }

        public MembershipCreateStatus CreateUser(string username, string password, string emailAddress)
        {
            MembershipCreateStatus createStatus;
            membershipProvider.CreateUser(username, password, emailAddress, null, null, true, null, out createStatus);

            return createStatus;
        }
    }

    public interface IRoleManager
    {
        bool IsEnabled { get; }
        bool DeleteRole(string name, bool throwOnPopulatedRole);
        IEnumerable<RoleDetails> GetAllRoles();
        void CreateRole(string roleName);
        IEnumerable<string> GetRolesForUser(string userName);
        bool IsUserInRole(string username, string roleName);
        void AddUserToRole(string username, string roleName);
        void RemoveUserFromRole(string username, string roleName);
    }

    public class RoleManager : IRoleManager
    {
        private readonly RoleProvider roleProvider;
        private readonly RoleManagerSection roleSection;
        private readonly IMapper<string, RoleDetails> roleMapper;

        public RoleManager(RoleProvider roleProvider, RoleManagerSection roleSection)
        {
            this.roleProvider = roleProvider;
            this.roleSection = roleSection;
            roleMapper = new UserRoleRoleDetailsMapper(roleProvider);
        }

        public bool IsEnabled { get { return roleSection.Enabled; } }

        public bool DeleteRole(string name, bool throwOnPopulatedRole)
        {
            return roleProvider.DeleteRole(name, throwOnPopulatedRole);
        }

        public IEnumerable<RoleDetails> GetAllRoles()
        {
            return roleMapper.MapAll(roleProvider.GetAllRoles());
        }

        public void CreateRole(string roleName)
        {
            roleProvider.CreateRole(roleName);
        }

        public IEnumerable<string> GetRolesForUser(string userName)
        {
            return roleProvider.GetRolesForUser(userName);
        }

        public bool IsUserInRole(string username, string roleName)
        {
            return roleProvider.IsUserInRole(username, roleName);
        }

        public void AddUserToRole(string username, string roleName)
        {
            roleProvider.AddUserToRole(username, roleName);
        }

        public void RemoveUserFromRole(string username, string roleName)
        {
            roleProvider.RemoveUserFromRole(username, roleName);
        }
    }

    public interface IProfileManager
    {
        bool IsEnabled { get; }
    }

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