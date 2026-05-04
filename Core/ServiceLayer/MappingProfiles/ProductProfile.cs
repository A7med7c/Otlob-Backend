using AutoMapper;
using DomainLayer.Models.Product;
using Shared.DTOs;

namespace ServiceImplementation.MappingProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dist => dist.BrandName, options => options.MapFrom(src => src.ProductBrand.Name))
            .ForMember(dist => dist.TypeName, options => options.MapFrom(src => src.ProductType.Name))
            .ForMember(dist => dist.ImageUrl, options => options.MapFrom<ImageResolver>());

        CreateMap<ProductType, TypeDto>();
        CreateMap<ProductBrand, BrandDto>();
    }
}
