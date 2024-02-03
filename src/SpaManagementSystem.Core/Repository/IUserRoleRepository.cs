using SpaManagementSystem.Core.Models;

namespace SpaManagementSystem.Core.Repository
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        Task<UserRole> GetByUserIdAsync(Guid userId);
        Task<UserRole> GetByRoleIdAsync(Guid roleId);
    }
}