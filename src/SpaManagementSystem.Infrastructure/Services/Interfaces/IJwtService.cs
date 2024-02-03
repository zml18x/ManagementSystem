using SpaManagementSystem.Core.Models;
using SpaManagementSystem.Infrastructure.Dto;

namespace SpaManagementSystem.Infrastructure.Services.Interfaces
{
    public interface IJwtService
    {
        JwtDto CreateToken(Customer user);
    }
}