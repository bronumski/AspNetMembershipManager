using System;
using System.Configuration;
using System.Configuration.Provider;
using System.IO;
using System.Reflection;
using System.Web.Profile;
using System.Web.Security;
using FluentAssertions;
using NUnit.Framework;

namespace AspNetMembershipManager.ProviderFactoryFixtures
{
	[TestFixture(TypeArgs = new[] { typeof(TestMembershipProvider) }, Description = "Membership Provider")]
	[TestFixture(TypeArgs = new[] { typeof(TestRoleProvider) }, Description = "Role Provider")]
	[TestFixture(TypeArgs = new[] { typeof(TestProfileProvider) }, Description = "Profile Provider")]
	class When_loading_a_provider_from_a_provider_configuration<TProviderInstance>
		where TProviderInstance : ProviderBase, ITestProvider
	{
		private IProviderFactory providerFactory;
		private ProviderBase provider;

		[SetUp]
		public void SetUp()
		{
			providerFactory = new ProviderFactory(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory);

			var settings = new ProviderSettings();
			settings.Name = "TestProvider";
			settings.Type = typeof(TProviderInstance).FullName;

			provider = providerFactory.CreateProviderFromConfig<TProviderInstance>(settings);
		}

		[Test]
		public void Should_load_membership_provider()
		{
			provider.Should().NotBeNull();
		}

		[Test]
		public void Should_be_instance_of_the_specified_provider()
		{
			provider.Should().BeOfType<TProviderInstance>();
		}

		[Test]
		public void Should_initialize_the_provider()
		{
			((TProviderInstance) provider).HasBeenInitialized.Should().BeTrue();
		}
	}

	interface ITestProvider
	{
		bool HasBeenInitialized { get; }
	}

#region TestMembershipProvider
	class TestMembershipProvider : MembershipProvider, ITestProvider
	{
		public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
		{
			HasBeenInitialized = true;
			base.Initialize(name, config);
		}

		public bool HasBeenInitialized { get; private set; }

		public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
		{
			throw new System.NotImplementedException();
		}

		public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
		{
			throw new System.NotImplementedException();
		}

		public override string GetPassword(string username, string answer)
		{
			throw new System.NotImplementedException();
		}

		public override bool ChangePassword(string username, string oldPassword, string newPassword)
		{
			throw new System.NotImplementedException();
		}

		public override string ResetPassword(string username, string answer)
		{
			throw new System.NotImplementedException();
		}

		public override void UpdateUser(MembershipUser user)
		{
			throw new System.NotImplementedException();
		}

		public override bool ValidateUser(string username, string password)
		{
			throw new System.NotImplementedException();
		}

		public override bool UnlockUser(string userName)
		{
			throw new System.NotImplementedException();
		}

		public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
		{
			throw new System.NotImplementedException();
		}

		public override MembershipUser GetUser(string username, bool userIsOnline)
		{
			throw new System.NotImplementedException();
		}

		public override string GetUserNameByEmail(string email)
		{
			throw new System.NotImplementedException();
		}

		public override bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			throw new System.NotImplementedException();
		}

		public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
		{
			throw new System.NotImplementedException();
		}

		public override int GetNumberOfUsersOnline()
		{
			throw new System.NotImplementedException();
		}

		public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new System.NotImplementedException();
		}

		public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new System.NotImplementedException();
		}

		public override bool EnablePasswordRetrieval
		{
			get { throw new System.NotImplementedException(); }
		}

		public override bool EnablePasswordReset
		{
			get { throw new System.NotImplementedException(); }
		}

		public override bool RequiresQuestionAndAnswer
		{
			get { throw new System.NotImplementedException(); }
		}

		public override string ApplicationName
		{
			get { throw new System.NotImplementedException(); }
			set { throw new System.NotImplementedException(); }
		}

		public override int MaxInvalidPasswordAttempts
		{
			get { throw new System.NotImplementedException(); }
		}

		public override int PasswordAttemptWindow
		{
			get { throw new System.NotImplementedException(); }
		}

		public override bool RequiresUniqueEmail
		{
			get { throw new System.NotImplementedException(); }
		}

		public override MembershipPasswordFormat PasswordFormat
		{
			get { throw new System.NotImplementedException(); }
		}

		public override int MinRequiredPasswordLength
		{
			get { throw new System.NotImplementedException(); }
		}

		public override int MinRequiredNonAlphanumericCharacters
		{
			get { throw new System.NotImplementedException(); }
		}

		public override string PasswordStrengthRegularExpression
		{
			get { throw new System.NotImplementedException(); }
		}
	}
#endregion

#region TestRoleProvider
	class TestRoleProvider : RoleProvider, ITestProvider
	{
		public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
		{
			HasBeenInitialized = true;
			base.Initialize(name, config);
		}

		public bool HasBeenInitialized { get; private set; }
		public override bool IsUserInRole(string username, string roleName)
		{
			throw new System.NotImplementedException();
		}

		public override string[] GetRolesForUser(string username)
		{
			throw new System.NotImplementedException();
		}

		public override void CreateRole(string roleName)
		{
			throw new System.NotImplementedException();
		}

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			throw new System.NotImplementedException();
		}

		public override bool RoleExists(string roleName)
		{
			throw new System.NotImplementedException();
		}

		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			throw new System.NotImplementedException();
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			throw new System.NotImplementedException();
		}

		public override string[] GetUsersInRole(string roleName)
		{
			throw new System.NotImplementedException();
		}

		public override string[] GetAllRoles()
		{
			throw new System.NotImplementedException();
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			throw new System.NotImplementedException();
		}

		public override string ApplicationName
		{
			get { throw new System.NotImplementedException(); }
			set { throw new System.NotImplementedException(); }
		}
	}
#endregion

#region TestProfileProvider
	class TestProfileProvider : ProfileProvider, ITestProvider
	{
		public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
		{
			HasBeenInitialized = true;
			base.Initialize(name, config);
		}

		public bool HasBeenInitialized { get; private set; }
		public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
		{
			throw new NotImplementedException();
		}

		public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
		{
			throw new NotImplementedException();
		}

		public override string ApplicationName
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}

		public override int DeleteProfiles(ProfileInfoCollection profiles)
		{
			throw new NotImplementedException();
		}

		public override int DeleteProfiles(string[] usernames)
		{
			throw new NotImplementedException();
		}

		public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
		{
			throw new NotImplementedException();
		}

		public override int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
		{
			throw new NotImplementedException();
		}

		public override ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}

		public override ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new NotImplementedException();
		}
	}
#endregion
}