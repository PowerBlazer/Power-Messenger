using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PowerMessenger.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services
        , [UsedImplicitly] IConfiguration configuration)
    {
        
        
        return services;
    }
}