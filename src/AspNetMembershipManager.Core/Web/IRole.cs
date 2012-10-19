namespace AspNetMembershipManager.Web
{
    public interface IRole
    {
        void Delete();
        string Name { get; }
        int UsersInRole { get; }
    }
}