using SpaManagementSystem.Core.Repository;
using SpaManagementSystem.Infrastructure.Data.Context;

namespace SpaManagementSystem.Infrastructure.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly SmsDbContext _context;



        public Repository(SmsDbContext context)
        {
            _context = context;
        }



        public async Task<TEntity> GetByIdAsync(Guid id)
            => await Task.FromResult(await _context.Set<TEntity>().FindAsync(id));

        public async Task CreateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), $"{nameof(entity)} cannot be a null");

            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), $"{nameof(entity)} cannot be a null");

            _context.Set<TEntity>().Update(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), $"{nameof(entity)} cannot be a null");

            _context.Set<TEntity>().Remove(entity);
        }

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}