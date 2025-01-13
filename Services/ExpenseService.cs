using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using System.Text.Json;


namespace ExpenseTracker.Services
{
    public class ExpenseService : IExpenseService
    {
        private List<IExpense> _expenses = new List<IExpense>();
        private string _fileName = "Expenses.json";
        private JsonSerializerOptions _jsonOptions;
        private IUser _currentUser;


        public ExpenseService()
        {
            _jsonOptions = Helper.InjectJsonOptionsBuilder();


        }


        public void Add(IExpense expense)
        {
            _expenses.Add(expense);
            Save();
        }


        public void Remove(IExpense expense)
        {
            if(_expenses.Count() == 1)
            {
                _expenses.Remove(expense);
                Save();
                return;
            }
            else
            {
                var biggerIds = _expenses.Where(e => e.Id > expense.Id);
                if (biggerIds.Count() == 0)
                {
                    _expenses.Remove(expense);
                    Save();
                    return;
                }
                else
                {
                    _expenses.Remove(expense);
                    foreach (var item in biggerIds)
                    {
                        item.Id = item.Id - 1;
                    }
                    Save();
                    return;
                }
            }

        }


        public void Save()
        {
            var expensesString = JsonSerializer.Serialize(_expenses, _jsonOptions);
            try
            {
                File.WriteAllText(_fileName, expensesString);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }



        public IEnumerable<IExpense> Load()
        {
            try
            {

                var expensesString = File.ReadAllText(_fileName);
                var expenseList = JsonSerializer.Deserialize<List<Expense>>(expensesString, _jsonOptions);
                if (expenseList != null)
                {
                    _expenses = expenseList.Cast<IExpense>().Where(e => e.UserId == _currentUser.Id).ToList();
                    return _expenses;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public IEnumerable<IExpense> GetExpensesByUserId(Guid userId)
        {
            Load();
            return _expenses.Where( e => e.UserId == userId );

        }


        public IEnumerable<IExpense> GetExpensesByMonth(Guid userId , int month)
        {
            return _expenses.Where(e => e.UserId == userId && e.Date.Month == month);
        }

        public IExpense GetExpenseById(int id)
        {
            return _expenses.FirstOrDefault(e => e.Id == id );
        }


        public IExpense CreateExpense(decimal amount, string description, Category? category, IUser user)
        {
            var id = GetLastExpenseId();
            var expense = new Expense(id + 1 , amount,description, category, user.Id);
            Add(expense);
            return expense;
        }




        public IUser SetLoggedInUser(IUser user)
        {
            _currentUser = user;
            return _currentUser;
        }


        public decimal CalculateTotal(Guid userId)
        {
         return GetExpensesByUserId(userId).Sum(e => e.Amount);
        }


        public decimal CalculateTotalByMonth(Guid userId , int month)
        {
            return _expenses.Where(e => e.UserId == userId && e.Date.Month == month).Sum(e => e.Amount);
        }
        public IEnumerable<Category> GetAllCategories()
        {
            return Enum.GetValues(typeof(Category)).Cast<Category>();
        }

        private int GetLastExpenseId()
        {
            var lastExpense = _expenses.LastOrDefault( e => e.UserId == _currentUser.Id );
            if(lastExpense != null)
            {
               return lastExpense.Id;
            }
            else
            {
                return 0;
            }
        }




    }
}
