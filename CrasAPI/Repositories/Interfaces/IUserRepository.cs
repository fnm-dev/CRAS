using CrasAPI.Model;

namespace CrasAPI.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> AddAsync(string username, string password);
    }
}
