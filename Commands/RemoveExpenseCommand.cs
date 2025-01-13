using ExpenseTracker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Commands
{
    public class RemoveExpenseCommand : ICommand
    {
        private readonly StateManager _stateManager;
        private readonly IExpenseService _expenseService;
        private IUser _currentUser;

        public RemoveExpenseCommand(StateManager stateManager, IExpenseService expenseService, IUser currentUser)
        {
            _stateManager = stateManager;
            _expenseService = expenseService;
            _currentUser = currentUser;
        }

        public void Execute()
        {
            if (_currentUser.IsLoggedIn)
            {
                Helper.ShowExpensesList(_expenseService.GetExpensesByUserId(_currentUser.Id) , null);
                Console.WriteLine("Enter the Id to remove: (Or [back] to go back to main menu )");
                var command = Console.ReadLine();
                if(command == "back")
                {
                    return;
                }
                else if(command != null || command != "")
                {
                   var isInt = int.TryParse(command, out var id);
                    if (isInt)
                    {
                        var expense = _expenseService.GetExpenseById(id);
                        if (expense != null)
                        {
                            _expenseService.Remove(expense);
                            Console.WriteLine("Expense Remove successfully");
                            Console.WriteLine("Press Any Key To Go back to main menu: ");
                            Console.ReadKey();
                            return;
                        }
                        else
                        { Console.WriteLine("There is no Expense with this id");
                            Console.WriteLine("Press Any Key To Go back to main menu: ");
                            Console.ReadKey();
                            return;
                        }
                    }
                    else
                    { Console.WriteLine("wrong input");
                        Console.WriteLine("Press Any Key To Go back to main menu: ");
                        Console.ReadKey();
                        return;
                    }

                }
                else
                {
                    Console.WriteLine("Wrong input!");
                    Console.WriteLine("Press Any Key To Go back to main menu: ");
                    Console.ReadKey();
                    return;
                }
            }
            else
            {
                Console.WriteLine("you must login first!");

            }
        }

    }
}
