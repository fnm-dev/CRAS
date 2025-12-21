using CrasAPI.Model;

namespace CrasAPI.Services.Results
{
    public class RegisterResult
    {
        public enum RegisterError
        {
            None,
            UsernameAlreadyExists,
            PasswordTooWeak
        }

        public bool Success { get; set; }
        public RegisterError? Error { get; set; }
        public User? User { get; set; }
    }
}
