using System;
using System.Collections.Generic;
using System.Configuration;

namespace AspNetMembershipManager.Web
{
	public interface IUser
	{
		string UserName { get; }
		string EmailAddress { get; set; }
		IEnumerable<IRole> Roles { get; }
		bool IsApproved { get; }
		bool IsLockedOut { get; }
		IEnumerable<SettingsPropertyValue> ProfileProperties { get; }
		string PasswordQuestion { get; }
		DateTime CreationDate { get; }
		DateTime LastLoginDate { get; }
		DateTime LastActivityDate { get; }
		DateTime LastLockoutDate { get; }
		DateTime LastPasswordChangedDate { get; }
		string Comment { get; set; }
		void Delete();
		void Save();
		void RemoveFromRole(IRole role);
		void AddToRole(IRole role);
		bool Unlock();
		bool ChangePassword(string password);
	}
}