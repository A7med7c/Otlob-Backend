using StackExchange.Redis;

namespace Persistence.Repositories;

internal class CashRepository(IConnectionMultiplexer connection) : ICashRepository
{
    readonly IDatabase database = connection.GetDatabase();
    public async Task<string?> GetAsync(string key)
    {
        var value = await database.StringGetAsync(key);
        return value.IsNullOrEmpty ? null : value.ToString();
    }

    public async Task SetAsync(string key, string value, TimeSpan lifeTime)
    {
        await database.StringSetAsync(key, value, lifeTime);
    }
}
