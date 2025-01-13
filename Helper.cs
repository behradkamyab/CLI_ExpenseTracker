

using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using System.Text.Json;

namespace ExpenseTracker
{
    /// <summary>
    /// Helper Class for check whether the input is string or integer and also inject some json options for json parser
    /// </summary>
    public static class Helper
    {
        public static bool IsInputNumber(string input)
        {
            return int.TryParse(input, out var result);

        }


        public static JsonSerializerOptions InjectJsonOptionsBuilder()
        {
            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            return options;
        }

        public static void ShowExpensesList(IEnumerable<IExpense> expenses , decimal? total)
        {
            const int dateWidth = 15;
            const int idWidth = 5;
            const int categoryWidth = 20;
            const int descriptionWidth = 20;
            const int amountWidth = 15;

            Console.WriteLine($"{"Date",-dateWidth}{"ID",-idWidth}{"Description",-descriptionWidth}{"Category",-categoryWidth}{"Amount",-amountWidth}");
            Console.WriteLine(new string('-', dateWidth + idWidth  + descriptionWidth + categoryWidth + amountWidth));

            foreach (var expense in expenses)
            {
                Console.WriteLine($"{expense.Date.ToShortDateString(),-dateWidth}" +
                             $"{expense.Id,-idWidth}" +
                             $"{expense.Description,-descriptionWidth}" +
                             $"{expense.Category,-categoryWidth}" +
                              $"{expense.Amount,-amountWidth:C}");
            }
            Console.WriteLine(new string('-', dateWidth + idWidth  + descriptionWidth + categoryWidth + amountWidth));
            if(total != null)
            {
                Console.WriteLine($"{"Total",-dateWidth - idWidth - descriptionWidth - categoryWidth}{total,amountWidth:C}");
            }


        }
    }
}
//var total = CalculateTotal(expenses);
//Console.WriteLine($"{"Total",-dateWidth - categoryWidth - descriptionWidth}{total,amountWidth:C}");