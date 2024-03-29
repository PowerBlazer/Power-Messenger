﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerMessenger.Application.Layers.Persistence;
using PowerMessenger.Application.Layers.Persistence.Context;
using PowerMessenger.Application.Layers.Persistence.Repositories;
using PowerMessenger.Application.Layers.Persistence.Repository;
using PowerMessenger.Infrastructure.Persistence.Context;
using PowerMessenger.Infrastructure.Persistence.Repositories;

namespace PowerMessenger.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructurePersistence(this IServiceCollection services
        ,IConfiguration configuration)
    {
        var connectionString = configuration["DB_APPLICATION_CONNECTION_STRING"] is null
            ? configuration.GetConnectionString("LocalDbApplication")
            : configuration["DB_APPLICATION_CONNECTION_STRING"]!;
        
        services.AddDbContext<IMessengerEfContext,MessengerEfContext>(options =>
        {
            options.UseNpgsql(
                connectionString,
                provider => provider.EnableRetryOnFailure()
            );
            
            options.UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IPersistenceUnitOfWork, PersistenceUnitOfWork>();
        services.AddScoped<IMessengerDapperContext>(_ => 
            new MessengerDapperContext(connectionString!));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IChatParticipantsRepository, ChatParticipantsRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IChatTypeRepository, ChatTypeRepository>();
        services.AddScoped<IMessageStatusRepository, MessageStatusRepository>();
        services.AddScoped<IMessageGroupChatRepository, MessageGroupChatRepository>();
        services.AddScoped<IMessageTypeRepository, MessageTypeRepository>();
        
        return services;
    }
}