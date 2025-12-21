namespace CrasAPI.DTO
{
    public class RegisterRequestDTO
    {
        public string Name { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
