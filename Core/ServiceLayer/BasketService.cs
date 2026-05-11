using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using SeviceAbstraction;
using Shared.DTOs.Basket;

namespace ServiceImplementation;

public class BasketService(IBasketRepository repository, IMapper mapper) : IBasketService
{
    public async Task<CustomerBasketDto> CreateorUpdateBasketAsync(CustomerBasketDto dto)
    {
        var basket = mapper.Map<CustomerBasketDto, CustomerBasket>(dto);

        var customerBasket = await repository.CreateBasketorUpdateAsync(basket);
        if (customerBasket is not null)
            return await GetBasketAsync(customerBasket.Id);
        else
            throw new Exception("Cant Create or Update Basket now");
    }

    public async Task<bool> DeleteBasketAsync(string key) => await repository.DeleteBasketAsync(key);

    public async Task<CustomerBasketDto> GetBasketAsync(string key)
    {
        var basket = await repository.GetBasketById(key);
        if (basket is not null)
            return mapper.Map<CustomerBasket, CustomerBasketDto>(basket);
        throw new BasketNotFoundException(key);
    }
}
