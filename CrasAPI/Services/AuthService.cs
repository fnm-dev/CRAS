using CrasAPI.Model;
using CrasAPI.Repository.Interfaces;
using CrasAPI.Services.Interfaces;
using CrasAPI.Services.Results;
using static CrasAPI.Services.Results.RegisterResult;
using static CrasAPI.Services.Results.LoginResult;

namespace CrasAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;

        public AuthService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<LoginResult> AuthenticateAsync(string username, string password)
        {
            var user = await _repository.GetByUsernameAsync(username);

            if (user == null)
                return new LoginResult
                {
                    Success = false,
                    Error = LoginError.UserNotFound
                };

            if (user.Password != password)
                return new LoginResult
                {
                    Success = false,
                    Error = LoginError.IncorrectCredentials
                };

            return new LoginResult
            {
                Success = true,
                User = user,
                Error = LoginError.None
            };
        }

        public async Task<RegisterResult> RegisterAsync(string username, string password)
        {
            var existing = await _repository.GetByUsernameAsync(username);
            if (existing != null)
                return new RegisterResult
                {
                    Success = false,
                    Error = RegisterError.UsernameAlreadyExists
                };

            if (password.Length < 6)
                return new RegisterResult
                {
                    Success = false,
                    Error = RegisterError.PasswordTooWeak
                };

            var user = await _repository.AddAsync(username, password);

            return new RegisterResult
            {
                Success = true,
                User = user,
                Error = RegisterError.None
            };
        }
    }
}
