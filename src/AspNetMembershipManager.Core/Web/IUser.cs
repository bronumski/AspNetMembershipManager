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
		DateTime CreationDate { get; }
		DateTime LastLoginDate { get; }
		IEnumerable<SettingsPropertyValue> ProfileProperties { get; }
		void Delete();
		void Save();
		void RemoveFromRole(IRole role);
		void AddToRole(IRole role);
	}
}