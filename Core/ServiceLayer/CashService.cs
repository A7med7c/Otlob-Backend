using DomainLayer.Contracts;
using SeviceAbstraction;
using System.Text.Json;

namespace ServiceImplementation;

public class CashService(ICashRepository cashRepository) : ICashService
{
    public async Task<string?> GetAsync(string key) => await cashRepository.GetAsync(key);

    public async Task SetAsync(string key, object value, TimeSpan lifeTime)
    {
        var stringValue = JsonSerializer.Serialize(value);
        await cashRepository.SetAsync(key, stringValue, lifeTime);
    }
}
