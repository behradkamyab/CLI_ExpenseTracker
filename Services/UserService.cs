using ExpenseTracker.Interfaces;
using ExpenseTracker.Models;
using System.Linq;
using System.Text.Json;


namespace ExpenseTracker.Services
{
    /// <summary>
    /// For simplicity Load and Save method are stored in this manager ( no repository pattern ).
    /// </summary>
    public class UserService : IUserService
    {
        private List<IUser> _users;
        private string _fileName = "Users.json";
        private JsonSerializerOptions _jsonOptions;

        public IEnumerable<IUser> Users { get { return _users; } private set { } }

        public UserService()
        {

            _users = new List<IUser>();
            _jsonOptions = Helper.InjectJsonOptionsBuilder();
            Users = Load();
        }





        public void Add(IUser user)
        {
            _users.Add(user);
            Save();

        }

        public void Remove(IUser user)
        {
            _users.Remove(user);
            Save();
        }


        public void Save()
        {
            var usersString = JsonSerializer.Serialize(_users, _jsonOptions);
            try
            {
                File.WriteAllText(_fileName, usersString);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }


        public IEnumerable<IUser> Load()
        {
            try
            {

                var usersString = File.ReadAllText(_fileName);
                var userList = JsonSerializer.Deserialize<IEnumerable<User>>(usersString, _jsonOptions);
                if (userList != null)
                {
                    _users = userList.Select(u => u as IUser).ToList();
                    return _users;
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


        public bool Register(string userName)
        {
            var userAvail = IsUserNameAvailableForUse(userName);
            if (userAvail)
            {
                var user = new User(userName);

                Add(user);
                return true;
            }
            else
            {
                return false;
            }

        }

        public IUser Login(string userName)
        {
            var user = _users.FirstOrDefault(u => u.UserName == userName);
            if (user != null)
            {
                user.IsLoggedIn = true;
                Save();
                return user;
            }
            else
            {
                return null;
            }
        }

       

        public bool IsUserNameAvailableForUse(string userName)
        {
            var isAvailable = _users.FirstOrDefault(u => u.UserName == userName);
            if (isAvailable != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void ResetAllUsers()
        {
            foreach(var user in _users)
            {
                user.IsLoggedIn = false;
            }
            Save();
        }
    }
}
