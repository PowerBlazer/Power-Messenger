using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerMessenger.Application.Layers.Persistence.Context;
using PowerMessenger.Infrastructure.Persistence.Context;

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
        
        return services;
    }
}