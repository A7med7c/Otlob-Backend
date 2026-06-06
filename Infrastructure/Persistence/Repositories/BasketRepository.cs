using DomainLayer.Models.BasketModule;
using StackExchange.Redis;
using System.Text.Json;

namespace Persistence.Repositories;

internal class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
{
    private readonly IDatabase _database = connection.GetDatabase();
    public async Task<CustomerBasket?> CreateBasketorUpdateAsync(CustomerBasket customerBasket, TimeSpan? lifeTime = null)
    {
        var basket = JsonSerializer.Serialize(customerBasket);
        var isCreatedorUpdated = await _database.StringSetAsync(customerBasket.Id, basket, lifeTime ?? TimeSpan.FromDays(15));
        if (isCreatedorUpdated)
            return await GetBasketById(customerBasket.Id);
        else
            return null;
    }

    public async Task<bool> DeleteBasketAsync(string key) => await _database.KeyDeleteAsync(key);

    public async Task<CustomerBasket?> GetBasketById(string key)
    {
        RedisValue basket = await _database.StringGetAsync(key);

        if (basket.IsNullOrEmpty)
        {
            return null;
        }

        return JsonSerializer.Deserialize<CustomerBasket>((ReadOnlySpan<byte>)basket);
    }
}
