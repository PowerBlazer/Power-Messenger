namespace PowerMessenger.Infrastructure.Identity.Interfaces;

public interface IIdentityUnitOfWork: IDisposable
{
    Task ExecuteWithExecutionStrategyAsync(Func<Task> action);
}