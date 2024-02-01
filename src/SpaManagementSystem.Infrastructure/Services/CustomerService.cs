using System.Security.Authentication;
using SpaManagementSystem.Core.Models;
using SpaManagementSystem.Core.Repository;
using SpaManagementSystem.Infrastructure.Dto;
using SpaManagementSystem.Infrastructure.Extensions.Repository;
using SpaManagementSystem.Infrastructure.Services.Interfaces;

namespace SpaManagementSystem.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IPasswordService _passwordService;



        public CustomerService(ICustomerRepository customerRepository, IPasswordService passwordService)
        {
            _customerRepository = customerRepository;
            _passwordService = passwordService;
        }



        public async Task<CustomerAccountDto> GetAsync(Guid id)
        {
            var customer = await _customerRepository.GetOrFailAsync(id);

            return new CustomerAccountDto(customer.Id, customer.Email, customer.PhoneNumber, customer.FirstName, customer.LastName, customer.Gender, customer.DateOfBirth,
                customer.Preferences, customer.EmailConfirmed, customer.PhoneNumberConfirmed, customer.TwoFactorEnabled);
        }

        public async Task RegisterAsync(string email, string password, string phoneNumber, string firstName, string lastName, string gender, DateOnly dateOfBirth, string? preferences)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email), "Email cannot be null");

            var emailWithoutSpaces = email.Trim(' ', '\n', '\t');

            var customer = await _customerRepository.GetByEmailAsync(emailWithoutSpaces);

            if (customer != null)
                throw new InvalidOperationException($"User with email '{email}' already exist");

            _passwordService.ValidatePassword(password);
            _passwordService.HashPassword(password, out byte[] passwordHash, out byte[] passwordSalt);

            var customerId = Guid.NewGuid();

            customer = new Customer(customerId, email, passwordSalt, passwordHash, phoneNumber, firstName, lastName, gender, dateOfBirth, preferences);

            await _customerRepository.CreateAsync(customer);
            await _customerRepository.SaveChangesAsync();
        }

        public async Task LoginAsync(string email, string password)
        {
            var customer = await _customerRepository.GetByEmailAsync(email);

            if (customer == null)
                throw new InvalidCredentialException("Invalid credentials");

            if (!_passwordService.VerifyPassword(password, customer.PasswordHash, customer.PasswordSalt))
                throw new InvalidCredentialException("Invalid credentials");
        }
    }
}