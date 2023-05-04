using Microsoft.Extensions.Configuration;
using PowerMessenger.Application.Layers.Redis;
using StackExchange.Redis;

namespace PowerMessenger.Infrastructure.Redis.Services;

public class RedisService: IRedisService
{
    private readonly IDatabase _dataBaseRedis;

    public RedisService(IDatabase dataBaseRedis)
    {
        _dataBaseRedis = dataBaseRedis;
    }

    public async Task<string?> GetValueAsync(string key)
    {
        return await _dataBaseRedis.StringGetAsync(key);
    }

    public async Task<bool> SetValueAsync(string key, string value, TimeSpan? expiry = null)
    {
        return await _dataBaseRedis.StringSetAsync(key, value);
    }

    public async Task<bool> DeleteValueAsync(string key)
    {
        return await _dataBaseRedis.KeyDeleteAsync(key);
    }

    public async Task<bool> UpdateValueAsync(string key, string value, TimeSpan? expiry = null)
    {
        if (await _dataBaseRedis.KeyExistsAsync(key))
        {
            return await _dataBaseRedis.StringSetAsync(key, value, expiry);
        }

        return false;
    }
}