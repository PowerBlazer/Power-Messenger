using Microsoft.EntityFrameworkCore;
using PowerMessenger.Infrastructure.Identity.Contexts;
using PowerMessenger.Infrastructure.Identity.Interfaces;

namespace PowerMessenger.Infrastructure.Identity;

public class IdentityUnitOfWork: IIdentityUnitOfWork
{
    private readonly IdentityContext _identityContext;
    public IdentityUnitOfWork(IdentityContext identityContext)
    {
        _identityContext = identityContext;
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
}