using SpaManagementSystem.Infrastructure.Dto;

namespace SpaManagementSystem.Infrastructure.Services.Interfaces
{
    public interface ICustomerService
    {    
        Task RegisterAsync(string email, string password, string phoneNumber, string firstName, string lastName, string gender, DateOnly dateOfBirth, string? preferences);
        Task<JwtDto> LoginAsync(string email, string password);
        Task<CustomerAccountDto> GetAsync(Guid id);
        Task UpdateProfileAsync(Guid id, string? firstName = null, string? lastName = null, string? gender = null, DateOnly? dateOfBirth = null);
    }
}