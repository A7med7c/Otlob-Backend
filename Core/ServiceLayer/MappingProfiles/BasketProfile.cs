using AutoMapper;
using DomainLayer.Models.BasketModule;
using Shared.DTOs.Basket;

namespace ServiceImplementation.MappingProfiles;

public class BasketProfile : Profile
{
    public BasketProfile()
    {
        CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
        CreateMap<BasketItem, BasketItemsDto>().ReverseMap();
    }
}
