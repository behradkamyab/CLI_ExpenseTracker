namespace ExpenseTracker.Interfaces
{
    public interface IUser
    {
        Guid Id { get; }
        string UserName { get; }
        bool IsLoggedIn { get; set; }




    }
}
