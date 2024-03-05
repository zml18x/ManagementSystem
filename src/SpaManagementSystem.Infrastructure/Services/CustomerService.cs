using System.Security.Authentication;
using SpaManagementSystem.Core.Models;
using SpaManagementSystem.Core.Repository;
using SpaManagementSystem.Infrastructure.Dto;
using SpaManagementSystem.Infrastructure.Exceptions;
using SpaManagementSystem.Infrastructure.Extensions.Repository;
using SpaManagementSystem.Infrastructure.Services.Interfaces;

namespace SpaManagementSystem.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;



        public CustomerService(ICustomerRepository customerRepository, IPasswordService passwordService, IJwtService jwtService)
        {
            _customerRepository = customerRepository;
            _passwordService = passwordService;
            _jwtService = jwtService;
        }



        /// <summary>
        /// Retrieves customer account information asynchronously.
        /// </summary>
        /// <param name="id">The ID of the customer.</param>
        /// <returns>A task representing the asynchronous operation, returning customer account information.</returns>
        public async Task<CustomerAccountDto> GetAsync(Guid id)
        {
            var customer = await _customerRepository.GetOrFailAsync(id);

            return new CustomerAccountDto(customer.Id, customer.Email, customer.PhoneNumber, customer.FirstName, customer.LastName, customer.Gender, customer.DateOfBirth,
                customer.Preferences, customer.EmailConfirmed, customer.PhoneNumberConfirmed, customer.TwoFactorEnabled);
        }

        /// <summary>
        /// Registers a new customer asynchronously.
        /// </summary>
        /// <param name="email">The email of the customer.</param>
        /// <param name="password">The password of the customer.</param>
        /// <param name="phoneNumber">The phone number of the customer.</param>
        /// <param name="firstName">The first name of the customer.</param>
        /// <param name="lastName">The last name of the customer.</param>
        /// <param name="gender">The gender of the customer.</param>
        /// <param name="dateOfBirth">The date of birth of the customer.</param>
        /// <param name="preferences">The preferences of the customer.</param>
        public async Task RegisterAsync(string email, string password, string phoneNumber, string firstName, string lastName, string gender, DateOnly dateOfBirth, string? preferences)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(nameof(email), "Email cannot be null");

            var normalizedEmail = email.Trim(' ', '\n', '\t');

            var customer = await _customerRepository.GetByEmailAsync(normalizedEmail);

            if (customer != null)
                throw new InvalidOperationException($"User with email '{email}' already exist");

            _passwordService.ValidatePassword(password);
            _passwordService.HashPassword(password, out byte[] passwordHash, out byte[] passwordSalt);

            var customerId = Guid.NewGuid();

            customer = new Customer(customerId, normalizedEmail, passwordSalt, passwordHash, phoneNumber, firstName, lastName, gender, dateOfBirth, preferences);

            await _customerRepository.CreateAsync(customer);
            await _customerRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Logs in a customer asynchronously and returns a JWT token.
        /// </summary>
        /// <param name="email">The email of the customer.</param>
        /// <param name="password">The password of the customer.</param>
        /// <returns>A task representing the asynchronous operation, returning a JWT token.</returns>
        public async Task<JwtDto> LoginAsync(string email, string password)
        {
            var customer = await _customerRepository.GetByEmailAsync(email);

            if (customer == null)
                throw new InvalidCredentialException("Invalid credentials");

            if (!_passwordService.VerifyPassword(password, customer.PasswordHash, customer.PasswordSalt))
                throw new InvalidCredentialException("Invalid credentials");

            var jwt = _jwtService.CreateToken(customer);

            return jwt;
        }

        /// <summary>
        /// Updates the profile information of a customer asynchronously.
        /// </summary>
        /// <param name="id">The ID of the customer to update.</param>
        /// <param name="firstName">The new first name of the customer.</param>
        /// <param name="lastName">The new last name of the customer.</param>
        /// <param name="gender">The new gender of the customer.</param>
        /// <param name="dateOfBirth">The new date of birth of the customer.</param>
        /// <param name="preferences">The new preferences of the customer.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateProfileAsync(Guid id, string? firstName = null, string? lastName = null, string? gender = null, DateOnly? dateOfBirth = null, string? preferences = null)
        {
            try
            {
                var customer = await _customerRepository.GetOrFailAsync(id);
                var isUpdated = customer.UpdateBasicInfromation(firstName, lastName, gender, dateOfBirth, preferences);

                if(isUpdated)
                {
                    await _customerRepository.UpdateAsync(customer);
                    await _customerRepository.SaveChangesAsync();
                }
            }
            catch (ArgumentNullException ex)
            {
                // ToDo

                throw;
            }
            catch (NotFoundException ex)
            {
                // ToDo

                throw;
            }
            catch (Exception ex)
            {
                // ToDo

                throw;
            }
        }
    }
}