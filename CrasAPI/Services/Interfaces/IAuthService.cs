using CrasAPI.DTO;
using CrasAPI.Model;
using CrasAPI.Services.Results;

namespace CrasAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResult> AuthenticateAsync(LoginRequestDTO dto);
        Task<RegisterResult> RegisterAsync(RegisterRequestDTO dto);
    }
}
