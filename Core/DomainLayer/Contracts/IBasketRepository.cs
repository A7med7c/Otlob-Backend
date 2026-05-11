using DomainLayer.Models.BasketModule;

namespace DomainLayer.Contracts;

public interface IBasketRepository
{
    Task<CustomerBasket?> GetBasketById(string key);
    Task<bool> DeleteBasketAsync(string key);
    Task<CustomerBasket?> CreateBasketorUpdateAsync(CustomerBasket customerBasket, TimeSpan? lifeTime = null);
}
