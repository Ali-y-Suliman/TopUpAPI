using Microsoft.EntityFrameworkCore;
using TopUpAPI.Data;
using TopUpAPI.Models;

namespace TopUpAPI.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> AddUser(User user)
        {
            _context.Users.Add(user);
            var id = await _context.SaveChangesAsync();
            var newUser = await _context.Users.FirstOrDefaultAsync(t => t.Id == id);
            if(newUser != null){
                return newUser;
            } else {
                throw new Exception("Failed to add user");
            }
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(t => t.Id == id);
            if(user != null){
                return user;
            } else {
                throw new Exception("Failed to add user");
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(t => t.Email == email);
            if(user != null){
                return user;
            } else {
                throw new Exception("Failed to add user");
            }
        }
    }
}
