using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace SpaManagementSystem.Infrastructure.Data.Context
{
    public class SmsDbContext : DbContext
    {
        public SmsDbContext() { }
        public SmsDbContext(DbContextOptions<SmsDbContext> options) : base(options) { }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("SMS");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}