using System.Linq.Expressions;
using PowerMessenger.Domain.Entities.Abstractions;

namespace PowerMessenger.Application.Layers.Persistence.Repository.Abstraction;

public interface IRepository<TEntity> where TEntity: BaseEntity<long>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> GetFirstOfDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> AddAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}