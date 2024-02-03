using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using SpaManagementSystem.Core.Models;
using SpaManagementSystem.Infrastructure.Dto;
using SpaManagementSystem.Infrastructure.Services.Interfaces;

namespace SpaManagementSystem.Infrastructure.Services
{
    /// <summary>
    /// Service for JWT token creation.
    /// </summary>
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;



        /// <summary>
        /// Initializes a new instance of the JwtService class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;

            ValidateConfiguration();
        }



        /// <summary>
        /// Creates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the token is created.</param>
        /// <returns>A JwtDto containing the JWT token and its expiration date.</returns>
        public JwtDto CreateToken(Customer user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(nameof(user), "User cannot be null.");

                if (user.Id == Guid.Empty)
                    throw new ArgumentException("User ID cannot be empty.", nameof(user.Id));


                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email)
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value!));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var expire = DateTime.UtcNow.AddMinutes(int.Parse(_configuration.GetSection("JWT:ExpiryMinutes").Value!));
                var issuer = _configuration.GetSection("JWT:Issuer").Value;
                var audience = _configuration.GetSection("JWT:Audience").Value;

                var token = new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: creds,
                    expires: expire,
                    issuer: issuer,
                    audience: audience);


                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                return new JwtDto(jwt, expire);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to create JWT token.", ex);
            }
        }

        /// <summary>
        /// Validates the JWT configuration settings.
        /// </summary>
        private void ValidateConfiguration()
        {
            if (string.IsNullOrEmpty(_configuration["JWT:Key"]))
                throw new InvalidOperationException("JWT Key is missing in configuration.");

            if (string.IsNullOrEmpty(_configuration["JWT:Issuer"]))
                throw new InvalidOperationException("JWT Issuer is missing in configuration.");

            if (string.IsNullOrEmpty(_configuration["JWT:Audience"]))
                throw new InvalidOperationException("JWT Audience is missing in configuration.");

            if (!int.TryParse(_configuration["JWT:ExpiryMinutes"], out int expiryMinutes) || expiryMinutes < 0)
                throw new InvalidOperationException("JWT ExpiryMinutes is not a valid positive integer.");
        }
    }
}