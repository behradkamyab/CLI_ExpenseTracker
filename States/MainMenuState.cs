using ExpenseTracker.Commands;

using ExpenseTracker.Interfaces;
using ExpenseTracker.Services;


namespace ExpenseTracker.States
{
    /// <summary>
    /// In Main State, You can decide to register or login
    /// </summary>
    public class MainMenuState : IState
    {

        private readonly StateManager _stateManager;
        private readonly IUserService _userService;
        private readonly IExpenseService _expenseService;

        public MainMenuState(StateManager stateManager, IUserService userService , IExpenseService expenseService)
        {
            _stateManager = stateManager;
            _expenseService = expenseService;
            _userService = userService;
        }

        public ICommand GetCommand()
        {
            var command = Console.ReadLine();
            switch (command)
            {
                case "register": return new RegisterCommand(_stateManager , _userService, _expenseService);
                case "login": return new LoginCommand(_stateManager, _userService, _expenseService);
                default: return new InvalidCommand();
            }



        }


        public void Render()
        {


            Console.WriteLine("Expense Tracker");
            Console.WriteLine("-----------------------------");

            Console.WriteLine("[login]");
            Console.WriteLine("[register]");




        }
    }
}
