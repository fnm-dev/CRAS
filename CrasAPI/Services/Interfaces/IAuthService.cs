using CrasAPI.DTO;
using CrasAPI.Model;
using CrasAPI.Services.Results;

namespace CrasAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResult> AuthenticateAsync(string username, string password);
        Task<RegisterResult> RegisterAsync(RegisterRequestDTO dto);
    }
}
