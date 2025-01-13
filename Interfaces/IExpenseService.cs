using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Interfaces
{
    public interface IExpenseService : IService<IExpense>
    {
        IExpense CreateExpense(decimal amount, string description, Category? category, IUser user);

        IUser SetLoggedInUser(IUser user);

        IEnumerable<Category> GetAllCategories();

        IEnumerable<IExpense> GetExpensesByUserId(Guid userId);

        IExpense GetExpenseById(int id);

        decimal CalculateTotal(Guid userId);
        decimal CalculateTotalByMonth(Guid userId , int month);

        IEnumerable<IExpense> GetExpensesByMonth(Guid userId, int month);
    }
}
