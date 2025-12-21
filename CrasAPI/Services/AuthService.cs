using CrasAPI.Model;
using CrasAPI.Repository.Interfaces;
using CrasAPI.Services.Interfaces;
using CrasAPI.Services.Results;
using static CrasAPI.Services.Results.RegisterResult;
using static CrasAPI.Services.Results.LoginResult;
using CrasAPI.DTO;

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

            await _repository.UpdateLastLoginAsync(user);

            return new LoginResult
            {
                Success = true,
                User = user,
                Error = LoginError.None
            };
        }

        public async Task<RegisterResult> RegisterAsync(RegisterRequestDTO dto)
        {
            var existing = await _repository.GetByUsernameAsync(dto.Username);
            if (existing != null)
                return new RegisterResult
                {
                    Success = false,
                    Error = RegisterError.UsernameAlreadyExists
                };

            if (dto.Password.Length < 6)
                return new RegisterResult
                {
                    Success = false,
                    Error = RegisterError.PasswordTooWeak
                };

            var user = new User
            {
                Username = dto.Username,
                Password = dto.Password,
                Name = dto.Name,
                FullName = dto.FullName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                LastPasswordUpdateAt = DateTime.UtcNow,
                IsActive = true,
                IsBlocked = false
            };

            user = await _repository.AddAsync(user);

            return new RegisterResult
            {
                Success = true,
                User = user,
                Error = RegisterError.None
            };
        }
    }
}
