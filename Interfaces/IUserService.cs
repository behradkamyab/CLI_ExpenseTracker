using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Interfaces
{
    public interface IUserService : IService<IUser>
    {
        bool Register(string userName);
        IUser Login(string userName);
        bool IsUserNameAvailableForUse(string userName);
    }
}
