using ExpenseTracker.Interfaces;
using ExpenseTracker.States;


namespace ExpenseTracker.Commands
{
    public class ListAllExpensesCommand : ICommand
    {
        private readonly StateManager _stateManager;

        private readonly IExpenseService _expenseService;
        private IUser _currentUser;
        private IEnumerable<IExpense>? _expenses;

        public ListAllExpensesCommand(StateManager stateManager, IExpenseService expenseService, IUser currentUser)
        {
            _stateManager = stateManager;
            _expenseService = expenseService;
            _currentUser = currentUser;
        }

        public void Execute()
        {
            if (_currentUser.IsLoggedIn)
            {
                Console.Clear();
                _expenses = _expenseService.GetExpensesByUserId(_currentUser.Id);
                if (_expenses == null || !_expenses.Any())
                {
                    Console.WriteLine("Empty List! Let's Add one!");
                    Console.WriteLine("Press Any Key To Go Back to Dashboard!");
                    Console.ReadKey();
                    return;
                }
                else
                {

                    Helper.ShowExpensesList( _expenses , null );
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("Press Any Key To Go Back to Dashboard!");
                    Console.ReadKey();

                }
            }
            else
            {
                Console.WriteLine("You must login first!");
            }
        }
    }
}
