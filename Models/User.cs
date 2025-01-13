using ExpenseTracker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Models
{
    public class User : IUser
    {
        public Guid Id { get;  set; }

        public string UserName { get;  set; }

        public bool IsLoggedIn { get; set; }


        public User()
        {

        }
        public User(string userName)
        {
            Id = Guid.NewGuid();
            UserName = userName;
        }
    }
}
