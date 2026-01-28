using CrasAPI.Models;
using CrasAPI.Repositories.Interfaces;
using CrasAPI.Services.Interfaces;

namespace CrasAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public Task<Customer?> AddAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            var customer = await _repository.GetByIdAsync(id);

            if (customer == null)
            {
                return null;
            }
        }

        public Task<List<Customer>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Customer?> UpdateAsync(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
