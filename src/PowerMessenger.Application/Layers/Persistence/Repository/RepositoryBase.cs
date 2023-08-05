using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PowerMessenger.Application.Layers.Persistence.Context;
using PowerMessenger.Application.Layers.Persistence.Repository.Abstraction;
using PowerMessenger.Domain.Entities.Abstractions;

namespace PowerMessenger.Application.Layers.Persistence.Repository;

public class RepositoryBase<TEntity>: IRepository<TEntity> 
    where TEntity: BaseEntity<long> 
{
    private readonly IMessengerEfContext _messengerEfContext;

    public RepositoryBase(IMessengerEfContext messengerEfContext)
    {
        _messengerEfContext = messengerEfContext;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
       var result = await _messengerEfContext
           .Set<TEntity>()
           .ToListAsync();

       return result;
    }

    public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var result = await _messengerEfContext
            .Set<TEntity>()
            .Where(predicate)
            .FirstAsync();

        return result;
    }

    public virtual async Task<TEntity?> GetFirstOfDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var result = await _messengerEfContext
            .Set<TEntity>()
            .Where(predicate)
            .FirstOrDefaultAsync();

        return result;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var result = await _messengerEfContext.Set<TEntity>().AddAsync(entity);
        await _messengerEfContext.SaveChangesAsync();

        return result.Entity;
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        _messengerEfContext.Attach(entity);
        _messengerEfContext.Set<TEntity>().Remove(entity);
        await _messengerEfContext.SaveChangesAsync();
    }
}