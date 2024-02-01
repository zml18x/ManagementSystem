using SpaManagementSystem.Core.Repository;
using SpaManagementSystem.Infrastructure.Exceptions;

namespace SpaManagementSystem.Infrastructure.Extensions.Repository
{
    public static class RepositoryExtensions
    {
        public static async Task<TEntity> GetOrFailAsync<TEntity>(this IRepository<TEntity> repository, Guid id) where TEntity : class
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id), "The id parameter cannot be empty");

            var entity = await repository.GetByIdAsync(id);

            if (entity == null)
                throw new NotFoundException($"Entity of type '{typeof(TEntity).Name}' with ID '{id}' does not exist");

            return entity;
        }
    }
}