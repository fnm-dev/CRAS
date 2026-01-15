using CrasAPI.Models;

namespace CrasAPI.Services.Results
{
    public class LoginResult
    {
        public enum LoginError
        {
            None,
            IncorrectCredentials,
            UserDeactivated,
            UserBlocked
        }

        public bool Success { get; set; }
        public LoginError? Error { get; set; }
        public User? User { get; set; }
    }
}
