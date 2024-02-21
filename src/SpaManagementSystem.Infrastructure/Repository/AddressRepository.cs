using Microsoft.EntityFrameworkCore;
using SpaManagementSystem.Core.Models;
using SpaManagementSystem.Core.Repository;
using SpaManagementSystem.Infrastructure.Data.Context;

namespace SpaManagementSystem.Infrastructure.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        private readonly SmsDbContext _context;



        public AddressRepository(SmsDbContext context) : base(context)
        {
            _context = context;
        }



        public async Task<Address?> GetByUserIdAsync(Guid userId)
            => await Task.FromResult(await _context.Addresses.FirstOrDefaultAsync(x => x.UserId == userId));
    }
}