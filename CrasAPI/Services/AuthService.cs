using CrasAPI.DTO;
using CrasAPI.Models;
using CrasAPI.Repository.Interfaces;
using CrasAPI.Services.Interfaces;
using CrasAPI.Services.Results;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static CrasAPI.Services.Results.LoginResult;
using static CrasAPI.Services.Results.RegisterResult;

namespace CrasAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        public async Task<LoginResult> AuthenticateAsync(LoginRequestDTO dto)
        {
            var user = await _repository.GetByUsernameAsync(dto.Username);

            if (user == null)
                return new LoginResult
                {
                    Success = false,
                    Error = LoginError.UserNotFound
                };

            if (user.Password != dto.Password)
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

        public string GenerateToken(User user, bool rememberMe)
        {
            var now = DateTime.UtcNow;

            var claims = new[]
            {
                new Claim("userId", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            }; 

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            int expireSeconds;
            if (rememberMe)
            {
                expireSeconds = int.Parse(_config["Jwt:RememberMeExpireSeconds"]!);
            }
            else
            {
                expireSeconds = int.Parse(_config["Jwt:ExpireSeconds"]!);
            }

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                notBefore: now,
                expires: now.AddSeconds(expireSeconds),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
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
                IsBlocked = false,
                AccessGroupId = 2 // Default access group
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
