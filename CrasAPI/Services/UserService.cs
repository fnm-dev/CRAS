using CrasAPI.Models;
using CrasAPI.Repository.Interfaces;
using CrasAPI.Services.Errors;
using CrasAPI.Services.Interfaces;
using CrasAPI.Services.Results;

namespace CrasAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<User>> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
                return Result<User>.Fail(ErrorCode.RecordNotFound);

            return Result<User>.Ok(user);
        }

        public async Task<Result<List<User>>> GetListAsync()
        {
            var users = await _repository.GetListAsync();

            if (users == null || users.Count == 0)
                return Result<List<User>>.Fail(ErrorCode.RecordNotFound);

            return Result<List<User>>.Ok(users);
        }
    }
}
