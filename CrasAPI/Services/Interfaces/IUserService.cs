using CrasAPI.Model;
using CrasAPI.Services.Results;

namespace CrasAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResult> GetListAsync();
        Task<UserResult> GetByIdAsync(int id);
    }
}
