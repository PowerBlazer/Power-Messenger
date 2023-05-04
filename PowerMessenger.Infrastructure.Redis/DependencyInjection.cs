using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerMessenger.Application.Layers.Redis;
using PowerMessenger.Infrastructure.Redis.Services;
using StackExchange.Redis;

namespace PowerMessenger.Infrastructure.Redis;

public static class DependencyInjection
{
    public static IServiceCollection AddRedis(this IServiceCollection services
        , [UsedImplicitly] IConfiguration configuration)
    {
        var redisConfiguration = configuration.GetSection("Redis").Get<RedisConfiguration>();

        var redisConnection = ConnectionMultiplexer.Connect($"{redisConfiguration!.Host}:{redisConfiguration.Port},password={redisConfiguration.Password}");
        
        services.AddSingleton<IRedisService>(_ => new RedisService(redisConnection.GetDatabase()));
        
        return services;
    }
}