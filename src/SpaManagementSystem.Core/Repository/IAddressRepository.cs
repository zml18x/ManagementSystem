using SpaManagementSystem.Core.Models;

namespace SpaManagementSystem.Core.Repository
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address> GetByUserId(Guid userId);
    }
}