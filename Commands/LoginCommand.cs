using ExpenseTracker.Interfaces;
using ExpenseTracker.Services;
using ExpenseTracker.States;

namespace ExpenseTracker.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly StateManager _stateManager;
        private readonly IUserService _userService;
        private readonly IExpenseService _expenseService;
        private string _userName;
        private IUser _currentUser;


        public LoginCommand(StateManager stateManager, IUserService userService, IExpenseService expenseService)
        {
            _stateManager = stateManager;
            _userService = userService;
            _expenseService = expenseService;
        }

        public void Execute()
        {
            while (true)
            {
                Console.WriteLine("Enter your user name: ");
                _userName = Console.ReadLine();
                if (_userName != "" && _userName != null && !Helper.IsInputNumber(_userName))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong user name ( not empty or number )");
                }
            }

            _currentUser = _userService.Login(_userName);
            if(_currentUser != null)
            {
                _expenseService.SetLoggedInUser(_currentUser);
                Console.WriteLine("You loggedIn successfully... Press Any Key To Go To The Dashboard!");
                Console.ReadKey();
                _stateManager.SwitchState(new DashboardState(_stateManager ,_userService, _expenseService ,_currentUser));
            }
            else
            {
                Console.WriteLine("Wrong user name or this user name doesnt exist!");

            }


        }
    }
}
