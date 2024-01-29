using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SpaManagementSystem.Infrastructure.Data.Context;

namespace SpaManagementSystem.Infrastructure.Data
{
    public static class DatabaseMigrator
    {
        /// <summary>
        /// Executes database migrations to ensure the database schema is up-to-date.
        /// If the database schema doesn't exist, it will be created. If the schema already exists,
        /// all available migrations will be applied to bring the schema to the latest version.
        /// </summary>
        /// <param name="serviceProvider">The service provider for dependency injection.</param>
        public static void MigrateDatabase(IServiceProvider serviceProvider)
        {
            // Migrate the main database context
            using (var scope = serviceProvider.CreateScope())
            {
                var scopeProvider = scope.ServiceProvider;
                var context = scopeProvider.GetRequiredService<SmsDbContext>();
                context.Database.Migrate();
            }
        }
    }
}