﻿namespace PowerMessenger.Application.Layers.Persistence;

public interface IPersistenceUnitOfWork
{
    Task ExecuteWithExecutionStrategyAsync(Func<Task> action);
}