using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerMessenger.Application.Layers.Shared.Services;
using PowerMessenger.Infrastructure.Shared.Services;

namespace PowerMessenger.Infrastructure.Shared;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureShared(this IServiceCollection services
        , IConfiguration configuration)
    {
        services.AddScoped<IChatService,ChatService>();
        
        
        return services;
    }
}