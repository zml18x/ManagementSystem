using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpaManagementSystem.Core.Repository;
using SpaManagementSystem.Infrastructure.Data.Context;
using SpaManagementSystem.Infrastructure.Repository;
using SpaManagementSystem.Infrastructure.Services;
using SpaManagementSystem.Infrastructure.Services.Interfaces;

namespace SpaManagementSystem.Infrastructure.Container
{
    public static class InfrastructureDependencies
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
            services.AddScoped<ICustomerRepository,CustomerRepository>();
            services.AddScoped<IAddressRepository,AddressRepository>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<ICustomerService, CustomerService>();


            services.AddDbContext<SmsDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("SpaManagementSystemConnection"));
            });
        }
    }
}