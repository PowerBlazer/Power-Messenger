using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerMessenger.Infrastructure.Identity.Contexts;

namespace PowerMessenger.Infrastructure.Identity;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureIdentity(this IServiceCollection services
        , [UsedImplicitly] IConfiguration configuration)
    {
        var connectionString = configuration["DB_IDENTITY_CONNECTION_STRING"] is null
            ? configuration.GetConnectionString("LocalDbIdentity")
            : configuration["DB_IDENTITY_CONNECTION_STRING"]!;
        
        services.AddDbContext<IdentityContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        
        return services;
    }
}