using Microsoft.EntityFrameworkCore;
using SpaManagementSystem.Core.Models;
using System.Reflection;

namespace SpaManagementSystem.Infrastructure.Data.Context
{
    public class SmsDbContext : DbContext
    {
        public SmsDbContext() { }
        public SmsDbContext(DbContextOptions<SmsDbContext> options) : base(options) { }



        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("SMS");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            modelBuilder.Entity<Address>()
                .HasOne<Customer>()
                .WithMany()
                .HasForeignKey(a => a.UserId);
        }
    }
}