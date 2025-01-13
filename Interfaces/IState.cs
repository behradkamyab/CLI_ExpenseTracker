namespace ExpenseTracker.Interfaces
{
    public interface IState
    {
        void Render();

        ICommand GetCommand();
    }
}
