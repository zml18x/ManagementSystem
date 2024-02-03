using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SpaManagementSystem.Core.Repository;
using SpaManagementSystem.Infrastructure.Data.Context;
using SpaManagementSystem.Infrastructure.Exceptions;
using SpaManagementSystem.Infrastructure.Repository;
using SpaManagementSystem.Infrastructure.Services;
using SpaManagementSystem.Infrastructure.Services.Interfaces;

namespace SpaManagementSystem.Infrastructure.Container
{
    /// <summary>
    /// Class responsible for configuring and registering infrastructure dependencies in the dependency injection container.
    /// </summary>
    public static class InfrastructureDependencies
    {
        /// <summary>
        /// Configures and registers infrastructure services and dependencies in the provided service collection.
        /// </summary>
        /// <param name="services">The service collection to which services will be added.</param>
        /// <param name="configuration">The configuration containing settings for the services.</param>
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Register repositories
            services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
            services.AddScoped<ICustomerRepository,CustomerRepository>();
            services.AddScoped<IAddressRepository,AddressRepository>();


            // Register services
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ICustomerService, CustomerService>();


            // Configure Entity Framework DbContext
            services.AddDbContext<SmsDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("SpaManagementSystemConnection"));
            });


            // Configure JWT authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration?["JWT:Key"] ?? throw new MissingConfigurationException("JWT key configuration is missing or empty")))
                };
            });
        }
    }
}