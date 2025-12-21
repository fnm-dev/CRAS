using System.ComponentModel.DataAnnotations;

namespace CrasAPI.DTO
{
    public class RegisterRequestDTO
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
