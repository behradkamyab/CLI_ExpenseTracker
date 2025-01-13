using ExpenseTracker.Services;
using ExpenseTracker.States;
using System.Reflection;
using System;
using System.Text.Json;

namespace ExpenseTracker
{
    /// <summary>
    ///  simple expense tracker application to manage your finances,allow users to add, delete, and view their expenses.
    ///  Using Json for storing objects
    ///  doesnt have any authentication service just a simple user manager for storing user names , save and load method ,check user name
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
               var userService = new UserService();

                AppDomain.CurrentDomain.ProcessExit += (sender, eventArgs) => userService.ResetAllUsers();

                var stateManager = new StateManager();
                var expenseService = new ExpenseService();
                stateManager.Run(new MainMenuState(stateManager, userService, expenseService));

        }

    }
}
