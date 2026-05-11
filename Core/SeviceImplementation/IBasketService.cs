using Shared.DTOs.Basket;

namespace SeviceAbstraction;

public interface IBasketService
{
    Task<CustomerBasketDto> GetBasketAsync(string key);
    Task<CustomerBasketDto> CreateorUpdateBasketAsync(CustomerBasketDto dto);
    Task<bool> DeleteBasketAsync(string key);
}
