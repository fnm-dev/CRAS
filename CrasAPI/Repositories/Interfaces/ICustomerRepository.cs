using CrasAPI.Models;

namespace CrasAPI.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        public Task<Customer?> GetByIdAsync(int id);
        public Task<List<Customer>> GetListAsync();
        public Task<Customer?> AddAsync(Customer customer);
        public Task<Customer?> UpdateAsync(Customer customer);
        public Task<bool> DeleteAsync(int id);
    }
}
