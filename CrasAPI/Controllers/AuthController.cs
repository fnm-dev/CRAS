using CrasAPI.Common;
using CrasAPI.DTO;
using CrasAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Serilog.Events;

namespace CrasAPI.Controllers
{
    [ApiController]
    [Route("/auth")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            Log(LogEventLevel.Information, "Validating login of user {User}", null, dto.Username);
            var user = await _service.AuthenticateAsync(dto.Username, dto.Password);
            if (user == null)
            {
                Log(LogEventLevel.Warning, "Login failed for user {User}", null, dto.Username);
                return Unauthorized(new { message = "Invalid username or password" });
            }
            return Ok(new { message = "Login successful", userId = user.Id });
        }
    }
}
