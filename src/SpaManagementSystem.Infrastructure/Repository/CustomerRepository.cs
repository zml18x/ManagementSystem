using Microsoft.EntityFrameworkCore;
using SpaManagementSystem.Core.Models;
using SpaManagementSystem.Core.Repository;
using SpaManagementSystem.Infrastructure.Data.Context;

namespace SpaManagementSystem.Infrastructure.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        private readonly SmsDbContext _context;



        public CustomerRepository(SmsDbContext context) : base(context)
        {
            _context = context;
        }



        public async Task<Customer> GetByEmailAsync(string email)
            => await Task.FromResult(await _context.Customers.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower()));
    }
}