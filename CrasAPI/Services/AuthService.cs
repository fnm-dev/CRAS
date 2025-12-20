using CrasAPI.Model;
using CrasAPI.Repository.Interfaces;
using CrasAPI.Services.Interfaces;

namespace CrasAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;

        public AuthService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var user = await _repository.GetByUsernameAsync(username);

            if (user == null)
                return null;

            if (user.Password != password)
                return null;

            return user;
        }
    }
}
