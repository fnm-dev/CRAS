using CrasAPI.Infrastructure;
using CrasAPI.Models;
using CrasAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrasAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Customer?> AddAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null) return false;

            _context.Customers.Remove(customer);

            return (await _context.SaveChangesAsync()) == 1;
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<List<Customer>> GetListAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer?> UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return customer;
        }
    }
}
