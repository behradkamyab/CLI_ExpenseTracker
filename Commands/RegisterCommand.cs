using ExpenseTracker.Interfaces;
using ExpenseTracker.Services;
using ExpenseTracker.States;


namespace ExpenseTracker.Commands
{
    public class RegisterCommand : ICommand
    {
        private readonly StateManager _stateManager;
        private readonly IUserService _userService;
        private readonly IExpenseService _expenseService;
        private string _userName;
        public RegisterCommand(StateManager stateManager, IUserService userService, IExpenseService expenseService)
        {
            _stateManager = stateManager;
            _userService = userService;
            _expenseService = expenseService;
        }


        public void Execute()
        {
            while (true)
            {
                Console.WriteLine("Enter your user name: ( Or [back] to go back to main menu ) ");
                _userName = Console.ReadLine();
                if(_userName == "back")
                {
                    return;
                }

                if(_userName != "" && _userName != null && !Helper.IsInputNumber(_userName))
                {
                    var isAvailable = _userService.IsUserNameAvailableForUse(_userName);
                    if (isAvailable)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("This user name is already exists! try another one!");
                    }

                }
                else
                {
                    Console.WriteLine("Wrong user name ( not empty or number )");
                }
            }

            var isRegistered = _userService.Register(_userName);
            if (isRegistered)
            {
                Console.WriteLine("Register successfully");
                Console.WriteLine("Press any key to go back to main menu!");
                Console.ReadKey();
                _stateManager.SwitchState(new MainMenuState(_stateManager , _userService, _expenseService));
            }
            else
            {
                Console.WriteLine("Something went wrong! try again!");
                _stateManager.SwitchState(new MainMenuState(_stateManager , _userService, _expenseService));
            }


        }
    }
}
