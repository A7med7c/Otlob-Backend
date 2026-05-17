using AutoMapper;
using DomainLayer.Models.OrderModule;
using Microsoft.Extensions.Configuration;
using Shared.DTOs.Order;

namespace ServiceImplementation.MappingProfiles
{
    internal class OrderItemPictureUrlResolver(IConfiguration _configuration) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
                return string.Empty;
            var url = $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.Product.PictureUrl}";
            return url;
        }
    }
}
