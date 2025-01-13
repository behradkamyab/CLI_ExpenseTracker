using ExpenseTracker.Commands;
using ExpenseTracker.Interfaces;


namespace ExpenseTracker.States
{
    public class DashboardState : IState
    {
        private readonly StateManager _stateManager;
        private readonly IUserService _userService;
        private readonly IExpenseService _expenseService;
        private IUser _currentUser;

        public DashboardState(StateManager stateManager  ,IUserService userService,IExpenseService expenseService ,IUser user)
        {
            _stateManager = stateManager;
            _userService = userService;
            _expenseService = expenseService;
            _currentUser = user;

        }
        public ICommand GetCommand()
        {
            if (_currentUser.IsLoggedIn)
            {
                var command = Console.ReadLine();
                switch (command)
                {
                    case "list": return new ListAllExpensesCommand(_stateManager, _expenseService, _currentUser);
                    case "add": return new AddExpenseCommand(_stateManager,_expenseService,_currentUser);
                    case "remove": return new RemoveExpenseCommand(_stateManager,_expenseService, _currentUser);
                    case "summary": return new SummaryExpensesCommand(_stateManager , _expenseService , _currentUser);
                    default: return new InvalidCommand();
                }
            }
            else
            {
                Console.WriteLine("You must login first!");
                return new InvalidCommand();
            }
        }

        public void Render()
        {
            if (_currentUser.IsLoggedIn)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Your Expense Tracker {0}" , _currentUser.UserName);
                Console.WriteLine("-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
                Console.WriteLine("[list] - to see all of your expenses details");
                Console.WriteLine("[add] - to add an expense");
                Console.WriteLine("[remove] - to remove an expense");
                Console.WriteLine("[summary] - to see the summary of all of your expenses");
            }
            else
            {
                Console.WriteLine("You must login first!");
            }

        }
    }
}
