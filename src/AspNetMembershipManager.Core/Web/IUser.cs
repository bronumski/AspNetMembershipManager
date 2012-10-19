namespace AspNetMembershipManager.Web
{
	public interface IUser
	{
		string UserName { get; }
		string EmailAddress { get; set; }
		void Delete();
		void Save();
	}
}