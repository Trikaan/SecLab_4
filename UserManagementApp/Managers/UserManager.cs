using Newtonsoft.Json;
using UserManagementApp.Models;

namespace UserManagementApp.Managers
{
    public class UserManager
    {
        private const string UsersFilePath = "users.json";
        private List<User> _users;

        public UserManager()
        {
            LoadUsers();
        }

        private void LoadUsers()
        {
            if (File.Exists(UsersFilePath))
            {
                var json = File.ReadAllText(UsersFilePath);
                var data = JsonConvert.DeserializeObject<Dictionary<string, List<User>>>(json);
                _users = data["users"];
            }
            else
            {
                _users = new List<User>
                {
                    new User
                    {
                        Username = "admin",
                        Password = "admin",
                        IsBlocked = false,
                        PasswordRestricted = false
                    }
                };
                SaveUsers();
            }
        }

        private void SaveUsers()
        {
            var data = new { users = _users };
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(UsersFilePath, json);
        }

        public User AuthenticateUser(string username, string password)
        {
            return _users.FirstOrDefault(u => 
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && 
                u.Password == password && 
                !u.IsBlocked);
        }

        public List<User> GetAllUsers()
        {
            return _users;
        }

        public void AddUser(User user)
        {
            _users.Add(user);
            SaveUsers();
        }

        public void UpdateUser(User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Username == user.Username);
            if (existingUser != null)
            {
                existingUser.Password = user.Password;
                existingUser.IsBlocked = user.IsBlocked;
                existingUser.PasswordRestricted = user.PasswordRestricted;
                SaveUsers();
            }
        }

        public void ChangePassword(string username, string newPassword)
        {
            var user = _users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                user.Password = newPassword;
                SaveUsers();
            }
        }
    }
} 