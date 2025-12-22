using CrasAPI.Models;
using CrasAPI.Repository.Interfaces;
using CrasAPI.Services.Interfaces;
using CrasAPI.Services.Results;
using static CrasAPI.Services.Results.UserResult;

namespace CrasAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserResult> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
                return new UserResult
                {
                    Success = false,
                    Error = UserError.RecordNotFound
                };

            return new UserResult
            {
                Success = true,
                User = user,
                Error = UserError.None
            };
        }

        public async Task<UserResult> GetListAsync()
        {
            var users = await _repository.GetListAsync();

            if (users == null || users.Count == 0)
                return new UserResult
                {
                    Success = false,
                    Error = UserError.RecordNotFound
                };

            return new UserResult
            {
                Success = true,
                Users = users,
                Error = UserError.None
            };
        }
    }
}
