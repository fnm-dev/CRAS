using CrasAPI.Models;

namespace CrasAPI.Services.Results
{
    public class LoginResult
    {
        public enum LoginError
        {
            None,
            UserNotFound,
            IncorrectCredentials
        }

        public bool Success { get; set; }
        public LoginError? Error { get; set; }
        public User? User { get; set; }
    }
}
