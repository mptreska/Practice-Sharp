using System.Collections.Generic;
using System.Linq;
using MedClinic.Models;

namespace MedClinic.Services
{
    public class AuthService
    {
        private JsonDataService jsonService = new JsonDataService();
        private List<User> users;

        public User CurrentUser { get; private set; }

        public AuthService()
        {
            users = jsonService.LoadUsers();
        }

        public bool Login(string username, string password)
        {
            var user = users.FirstOrDefault(u =>
                u.Username == username && u.Password == password);

            if (user != null)
            {
                CurrentUser = user;
                return true;
            }
            return false;
        }

        public bool Register(string username, string password,
                             string fullName, UserRole role)
        {
            if (users.Any(u => u.Username == username))
                return false;

            var newUser = new User
            {
                Id = users.Count + 1,
                Username = username,
                Password = password,
                FullName = fullName,
                Role = role
            };

            users.Add(newUser);
            jsonService.SaveUsers(users);
            return true;
        }

        public void Logout() => CurrentUser = null;
    }
}