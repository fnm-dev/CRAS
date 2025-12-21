using CrasAPI.Model;

namespace CrasAPI.Services.Results
{
    public class UserResult
    {
        public enum UserError
        {
            None,
            RecordNotFound
        }

        public bool Success { get; set; }
        public UserError? Error { get; set; }
        public User? User { get; set; }
        public List<User>? Users { get; set; }
    }
}
