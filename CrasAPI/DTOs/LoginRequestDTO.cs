using System.ComponentModel.DataAnnotations;

namespace CrasAPI.DTO
{
    public class LoginRequestDTO
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required] 
        public string Password { get; set; } = null!;
        }
}
