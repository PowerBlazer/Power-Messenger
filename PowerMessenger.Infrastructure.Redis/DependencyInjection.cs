using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerMessenger.Application.Layers.Redis;
using PowerMessenger.Application.Layers.Redis.Services;
using PowerMessenger.Infrastructure.Redis.Services;

namespace PowerMessenger.Infrastructure.Redis;

public static class DependencyInjection
{
    public static IServiceCollection AddRedis(this IServiceCollection services
        , [UsedImplicitly] IConfiguration configuration)
    {
        var redisConfiguration = configuration.GetSection("Redis").Get<RedisConfiguration>();
        
        services.AddScoped<IRedisService>(_ => new RedisService(redisConfiguration!));
        
        return services;
    }
}