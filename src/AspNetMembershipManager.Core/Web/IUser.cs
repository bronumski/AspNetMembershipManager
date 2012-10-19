using System.Collections.Generic;

namespace AspNetMembershipManager.Web
{
	public interface IUser
	{
		string UserName { get; }
		string EmailAddress { get; set; }
		IEnumerable<IRole> Roles { get; }
		void Delete();
		void Save();
	}
}