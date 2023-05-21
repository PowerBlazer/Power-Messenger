namespace PowerMessenger.Infrastructure.Identity.Interfaces;

public interface IIdentityUnitOfWork
{
    Task ExecuteWithExecutionStrategyAsync(Func<Task> action);
}