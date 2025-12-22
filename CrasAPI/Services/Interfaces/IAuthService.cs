using CrasAPI.DTO;
using CrasAPI.Models;
using CrasAPI.Services.Results;

namespace CrasAPI.Services.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(User user, bool rememberMe);
        Task<LoginResult> AuthenticateAsync(LoginRequestDTO dto);
        Task<RegisterResult> RegisterAsync(RegisterRequestDTO dto);
    }
}
