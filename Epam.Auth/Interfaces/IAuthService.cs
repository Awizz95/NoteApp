using Epam.Auth.Models;

namespace Epam.Auth.Interfaces
{
    public interface IAuthService
    {
        User? GetUserByLoginAndPassword(string login, string password);
        bool RegisterUser(User user);
        User? Login(User user);
        bool IsUserInRole(Guid id, RoleEnum role);
        List<User> GetAllUsers();
        User? GetUserById(Guid id);
        User? GetUserByLogin(string login);
        bool DeleteUser(Guid id);
    }
}
