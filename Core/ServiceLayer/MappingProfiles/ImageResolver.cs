using AutoMapper;
using DomainLayer.Models.Product;
using Microsoft.Extensions.Configuration;
using Shared.DTOs;

namespace ServiceImplementation.MappingProfiles;

public class ImageResolver(IConfiguration _configuration) : IValueResolver<Product, ProductDto, string>
{
    public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
    {
        if (string.IsNullOrEmpty(source.PictureUrl))
            return string.Empty;
        var url = $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.PictureUrl}";
        return url;
    }
}


