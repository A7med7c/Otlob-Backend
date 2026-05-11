using AutoMapper;
using DomainLayer.Models;
using DomainLayer.Models.BasketModule;
using Shared.DTOs;
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
