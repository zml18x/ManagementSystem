﻿using SpaManagementSystem.Infrastructure.Dto;

namespace SpaManagementSystem.Infrastructure.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerAccountDto> GetAsync(Guid id);
        Task RegisterAsync(string email, string password, string phoneNumber, string firstName, string lastName, string gender, DateOnly dateOfBirth, string? preferences);
        Task LoginAsync(string email, string password);
    }
}