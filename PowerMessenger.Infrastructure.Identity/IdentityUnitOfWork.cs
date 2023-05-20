using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PowerMessenger.Infrastructure.Identity.Contexts;
using PowerMessenger.Infrastructure.Identity.Interfaces;

namespace PowerMessenger.Infrastructure.Identity;

public class IdentityUnitOfWork: IIdentityUnitOfWork
{
    private readonly IdentityContext _identityContext;
    private  IDbContextTransaction? _transaction;

    public IdentityUnitOfWork(IdentityContext identityContext)
    {
        _identityContext = identityContext;
    }

    public void Dispose()
    {
        _transaction?.Dispose();
    }

    public async Task ExecuteWithExecutionStrategyAsync(Func<Task> action)
    {
        var executionStrategy = _identityContext.Database.CreateExecutionStrategy();

        await executionStrategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _identityContext.Database.BeginTransactionAsync(); 

            try
            {
                await action();
                await transaction.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw new Exception(e.Message);
            }
        });
    }

    public async Task BeginTransactionAsync()
    { 
        _transaction = await _identityContext.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        await _transaction?.CommitAsync()!;
    }

    public async Task RollbackAsync()
    {
        await _transaction?.RollbackAsync()!;
    }
}