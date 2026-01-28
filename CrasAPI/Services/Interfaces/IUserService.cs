using CrasAPI.Models;
using CrasAPI.Services.Results;

namespace CrasAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<Result<List<User>>> GetListAsync();
        Task<Result<User>> GetByIdAsync(int id);
    }
}
