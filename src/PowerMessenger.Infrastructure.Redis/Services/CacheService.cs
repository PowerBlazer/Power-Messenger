using System.Text.Json;
using PowerMessenger.Application.Layers.Redis.Services;

namespace PowerMessenger.Infrastructure.Redis.Services;

public class CacheService: ICacheService
{
    private readonly IRedisService _redisService;

    public CacheService(IRedisService redisService)
    {
        _redisService = redisService;
    }
    
    public async Task<T?> GetValue<T>(string key)
    {
        var value = await _redisService.GetValueAsync(key);

        return value is not null 
            ? JsonSerializer.Deserialize<T>(value) 
            : default;
    }

    public async Task<bool> SetData<T>(string key, T value, TimeSpan expiry)
    {
        var serializeObject = JsonSerializer.Serialize(value);

        var isSet = await _redisService.SetValueAsync(key, serializeObject, expiry);

        return isSet;
    }
}