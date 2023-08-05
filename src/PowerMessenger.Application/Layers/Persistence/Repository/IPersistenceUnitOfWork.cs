namespace PowerMessenger.Application.Layers.Persistence.Repository;

public interface IPersistenceUnitOfWork
{
    Task ExecuteWithExecutionStrategyAsync(Func<Task> action);
}