using CrasAPI.Data;
using CrasAPI.Model;
using CrasAPI.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrasAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> AddAsync(string username, string password)
        {
            var user = new User
            {
                Username = username,
                Password = password
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
