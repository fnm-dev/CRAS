using CrasAPI.Model;

namespace CrasAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User?> AuthenticateAsync(string username, string password);
    }
}
