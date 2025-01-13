using ExpenseTracker.Interfaces;


namespace ExpenseTracker.Commands
{
    public class InvalidCommand : ICommand
    {



        public void Execute()
        {
            Console.WriteLine("Invalid Command");

        }
    }
}
