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

            CreateMap<Order, ReturnedOrderDto>()
<<<<<<< HEAD
                .ForMember(d => d.DeliveyMethod, o => o.MapFrom(s => s.DeliveyMethod.ShortName));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<DeliveryMethod, DeliveryMethodDto>().ReverseMap();
=======
                .ForMember(d => d.BuyerEmail, o => o.MapFrom(s => s.UserEmail))
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveyMethod.ShortName))
                .ForMember(d => d.DeliveryCost, o => o.MapFrom(s => s.DeliveyMethod.Price))
                .ForMember(d => d.ShipToAddress, o => o.MapFrom(s => s.ShippingAddress))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.OrderStatus.ToString()))
                .ForMember(d => d.Total, o => o.MapFrom(s => s.GetTotal));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId.ToString()))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());

            CreateMap<DeliveryMethod, DeliveryMethodDto>()
                .ForMember(d => d.Cost, o => o.MapFrom(s => s.Price));
>>>>>>> origin/Dev
        }
    }
}
