using TopUpAPI.Models;

namespace TopUpAPI.Repositories.UserRepository
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<User> GetUser(int id);
        Task<User> GetUserByEmail(string email);
    }
}
