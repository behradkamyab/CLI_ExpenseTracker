using ExpenseTracker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Commands
{
    public class SummaryExpensesCommand : ICommand
    {
        private readonly StateManager _stateManager;

        private readonly IExpenseService _expenseService;
        private IUser _currentUser;
        private IEnumerable<IExpense>? _expenses;
        private int _month;


        public SummaryExpensesCommand(StateManager stateManager, IExpenseService expenseService, IUser currentUser)
        {
            _stateManager = stateManager;
            _expenseService = expenseService;
            _currentUser = currentUser;
        }

        public void Execute()
        {
            if (_currentUser.IsLoggedIn)
            {
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
                    Console.WriteLine("Do you want to filter the total of your expenses based on specific month? y/n (if you choose no the total of all will be displayed)");
                    var command = Console.ReadLine();
                    if (command == "y")
                    {
                        while (true)
                        {
                            Console.WriteLine("Enter the month: ");
                            if(int.TryParse(Console.ReadLine(), out  _month) && _month > 0 && _month <= 12)
                            {
                                break;
                            }

                        }
                        _expenses = _expenseService.GetExpensesByMonth(_currentUser.Id, _month);
                        if (_expenses == null || !_expenses.Any())
                        {
                            Console.WriteLine("There is no specific expense into the {0} month of the year", _month);
                            Console.WriteLine("Press Any Key To Go Back to Dashboard!");
                            Console.ReadKey();
                            return;
                        }
                        else
                        {
                            var total = _expenseService.CalculateTotalByMonth(_currentUser.Id, _month);
                            Helper.ShowExpensesList(_expenses, total);
                            Console.WriteLine("-------------------------------------------------");
                            Console.WriteLine("Press Any Key To Go Back to Dashboard!");
                            Console.ReadKey();
                            return;
                        }



                    }
                    else if(command == "n")
                    {
                        decimal total = _expenseService.CalculateTotal(_currentUser.Id);

                        Helper.ShowExpensesList(_expenses, total);
                        Console.WriteLine("-------------------------------------------------");
                        Console.WriteLine("Press Any Key To Go Back to Dashboard!");
                        Console.ReadKey();
                        return;
                    }
                    else
                    {
                        Console.WriteLine("wrong input");
                        Console.WriteLine("Press any key to go back to main menu");
                        Console.ReadKey();
                        return;
                    }


                }

            }
            else
            {
                Console.WriteLine("You must login first!");
            }
        }
    }
}
