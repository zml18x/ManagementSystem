using SpaManagementSystem.Core.Models;

namespace SpaManagementSystem.Core.Repository
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role> GetByName(string name);
    }
}