using Microsoft.EntityFrameworkCore;
using PowerMessenger.Application.Layers.Persistence;
using PowerMessenger.Application.Layers.Persistence.Context;
using PowerMessenger.Application.Layers.Persistence.Repository;

namespace PowerMessenger.Infrastructure.Persistence;

public class PersistenceUnitOfWork: IPersistenceUnitOfWork
{
    private readonly IMessengerEfContext _messengerEfContext;

    public PersistenceUnitOfWork(IMessengerEfContext messengerEfContext)
    {
        _messengerEfContext = messengerEfContext;
    }

    public async Task ExecuteWithExecutionStrategyAsync(Func<Task> action)
    {
        var executionStrategy = _messengerEfContext.Database.CreateExecutionStrategy();

        await executionStrategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _messengerEfContext.Database.BeginTransactionAsync(); 

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
}