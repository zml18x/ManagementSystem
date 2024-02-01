using SpaManagementSystem.Core.Models;

namespace SpaManagementSystem.Core.Repository
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        Task<UserRole> GetByUserId(Guid userId);
        Task<UserRole> GetByRoleId(Guid roleId);
    }
}