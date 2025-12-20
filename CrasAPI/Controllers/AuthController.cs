using CrasAPI.DTO;
using CrasAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CrasAPI.Controllers
{
    [ApiController]
    [Route("/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var user = await _service.AuthenticateAsync(dto.Username, dto.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }
            return Ok(new { message = "Login successful", userId = user.Id });
        }
    }
}
