using Epam.Auth.Interfaces;
using Epam.Auth.Models;

namespace Epam.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly List<User> users = new();

        private static User AddRoleToUser(User user, RoleEnum role)
        {
            user.Role = role;

            return user;
        }

        public User? GetUserByLoginAndPassword(string login, string password)
        {
            return users.SingleOrDefault(x => x.Login.Equals(login) && x.Password.Equals(password));
        }

        public User? GetUserById(Guid id)
        {
            return users.SingleOrDefault(x => x.Id.Equals(id));
        }

        public User? GetUserByLogin(string login)
        {
            return users.SingleOrDefault(x => x.Login.Equals(login));
        }

        public bool RegisterUser(User user)
        {
            User person = user;

            if (users.Any(x => x.Login == user.Login))
            {
                return false;
            }

            if (user.Login == "Admin" && user.Password == "Admin")
            {
                person = AddRoleToUser(user, RoleEnum.Admin);
            }

            users.Add(person);

            return true;
        }

        public User? Login(User user)
        {
            return users.SingleOrDefault(x => x.Login == user.Login && x.Password == user.Password);
        }

        public bool IsUserInRole(Guid id, RoleEnum role)
        {
            var user = GetUserById(id);

            return user != null && user.Role == role;
        }

        public List<User> GetAllUsers()
        {
            return users;
        }

        public bool DeleteUser(Guid id)
        {
            User? user = GetUserById(id);

            return users.Remove(user);
        }
    }
}
