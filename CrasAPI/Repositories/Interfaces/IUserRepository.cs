using CrasAPI.Models;

namespace CrasAPI.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task UpdateLastLoginAsync(User user);
        Task<User?> AddAsync(User user);
        Task<List<User>> GetListAsync();
        Task<User?> GetByIdAsync(int id);
    }
}
