using System.Text.Json;
using StackExchange.Redis;

namespace OrderService.Services.RedisCache;

public interface IRedisCacheService
{
    Task<long> Enqueue<T>(string key, T value);
    Task<T> Dequeue<T>(string key);
}
public class RedisCacheService : IRedisCacheService
{
    private readonly IDatabase _redisDatabase;
    private readonly ConnectionMultiplexer _connectionMultiplexer;

    public RedisCacheService()
    {
        ConfigurationOptions option = new ConfigurationOptions
        {
            
            AbortOnConnectFail = false,
            EndPoints = { "localhost:6379"}
        };
        _connectionMultiplexer = ConnectionMultiplexer.Connect(option);
        _redisDatabase = _connectionMultiplexer.GetDatabase();
    }

    public async Task<long> Enqueue<T>(string queue, T value)
        => await _redisDatabase.ListRightPushAsync("shipper", $"shipper-{value}");

    public async Task<T> Dequeue<T>(string queue)
    {
        var value = await _redisDatabase.ListLeftPopAsync(queue);
        if (!value.HasValue)
        {
        }

        return default;
    }
}
