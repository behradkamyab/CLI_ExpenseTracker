using ExpenseTracker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Models
{
    public class Expense : IExpense
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount{ get; set; }

        public string Description { get; set; }

        public Category? Category { get; set; }

        public Guid UserId { get; set; }




        public Expense(int id,decimal amount, string description, Category? category, Guid userId)
        {
            Id = id;
            Date = DateTime.Now;
            Amount = amount;
            Description = description;
            Category = category;
            UserId = userId;
        }
    }
}
