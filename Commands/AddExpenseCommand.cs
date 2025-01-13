using ExpenseTracker.Interfaces;
using ExpenseTracker.Services;
using ExpenseTracker.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Commands
{
    public class AddExpenseCommand : ICommand
    {
        private readonly StateManager _stateManager;
        private readonly IExpenseService _expenseService;
        private IUser _currentUser;
        private decimal _amount;
        private string _description;
        private Category? _category;
        private IEnumerable<Category> _categories;


        public AddExpenseCommand(StateManager stateManager ,IExpenseService expenseService,IUser currentUser)
        {
            _stateManager = stateManager;
            _expenseService = expenseService;
            _currentUser = currentUser;
            _categories = _expenseService.GetAllCategories();
        }

        public void Execute()
        {

            if (_currentUser.IsLoggedIn)
            {
                bool isRunning = true;

                while (true)
                {
                    Console.WriteLine("Enter the description:  ( [back] - to go back to the main menu )");
                    _description = Console.ReadLine();
                    if(_description == "back")
                    {
                        return;
                    }

                    if (_description != "" && _description != null)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Wrong input");
                    }

                }
                while (true)
                {
                    Console.WriteLine("Enter the amount of $$: ");
                    var result = Console.ReadLine();
                   var isValidAmount = decimal.TryParse(result, out _amount);
                    if (isValidAmount)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Wrong input");
                    }

                }

                while (isRunning)
                {
                    _category = null;
                    Console.WriteLine("Need any specific Category? y/n");
                    var command = Console.ReadLine();
                    switch (command)
                    {
                        case "y": _category = RenderAndSelectCatagories(); isRunning = false; break;
                        case "n": isRunning = false; break;
                        default: Console.WriteLine("invalid input");break;
                    }
                }


                var expense = _expenseService.CreateExpense(_amount, _description,_category, _currentUser);
                if (expense != null)
                {
                        Console.WriteLine("Expense added successfully");
                        Console.WriteLine("Press Any Key to go to the dashboard");
                        Console.ReadKey();
                        return;
                }
                else
                {
                    Console.WriteLine("Something went wrong");
                }



            }
            else
            {
                Console.WriteLine("You must login first");

            }

        }


        private Category RenderAndSelectCatagories()
        {
            foreach (var item in _categories)
            {
                Console.WriteLine(item);
            }
            while (true)
            {
                Console.WriteLine("Which one you want to choose? ( Enter the first letter of your choice ): ");
                var command = Console.ReadLine();
                switch (command)
                {
                    case "h": return Category.Housing;
                    case "u": return Category.Utilities;
                    case "t": return Category.Transportation;
                    case "m": return Category.Medical;
                    case "d": return Category.Debt;
                    case "e": return Category.Education;
                    case "s": return Category.Savings;
                    case "i": return Category.Investments;
                    case "g": return Category.General;
                }
            }

        }
    }
}
