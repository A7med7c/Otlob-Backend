using AutoMapper;
using DomainLayer.Models.OrderModule;
using Shared.DTOs.Identity;
using Shared.DTOs.Order;

namespace ServiceImplementation.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDto, ShippingAddress>().ReverseMap();

            CreateMap<Order, CreatedOrderDto>()
                .ForMember(d => d.DeliveyMethod, o => o.MapFrom(s => s.DeliveyMethod.ShortName));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());

        }
    }
}
