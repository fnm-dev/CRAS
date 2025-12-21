using Microsoft.AspNetCore.Mvc;
using Serilog.Events;
using CrasAPI.Common;
using CrasAPI.DTO;
using CrasAPI.Services.Interfaces;
using static CrasAPI.Services.Results.RegisterResult;
using static CrasAPI.Services.Results.LoginResult;

namespace CrasAPI.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO dto, [FromQuery] bool rememberMe = false)
        {
            Log(LogEventLevel.Information, "Validating login of user {User}", null, dto.Username);
            var result = await _service.AuthenticateAsync(dto);

            if (!result.Success)
            {
                Log(LogEventLevel.Warning, "Login failed for user {User}", null, dto.Username);
                return result.Error switch
                {
                    LoginError.UserNotFound =>
                        Unauthorized(new { message = "User not found" }),

                    LoginError.IncorrectCredentials =>
                        Unauthorized(new { message = "Incorrect credentials" }),

                    _ => BadRequest(new { message = "Internal Error" })
                };
            }

            var token = _service.GenerateToken(result.User, rememberMe);

            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDTO dto)
        {
            var result = await _service.RegisterAsync(dto);

            if (!result.Success)
            {
                return result.Error switch
                {
                    RegisterError.UsernameAlreadyExists =>
                        BadRequest(new { message = "Username already exists" }),

                    RegisterError.PasswordTooWeak =>
                        BadRequest(new { message = "Password too weak" }),

                    _ => BadRequest(new { message = "Internal Error" })
                };
            }

            return Ok(new
            {
                message = "Register successful",
                userId = result.User.Id
            });
        }
    }
}
