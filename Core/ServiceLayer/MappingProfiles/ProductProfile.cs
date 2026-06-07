using AutoMapper;
using DomainLayer.Models.Product;
using Shared.DTOs.Product;

namespace ServiceImplementation.MappingProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>()
<<<<<<< HEAD
            .ForMember(dist => dist.BrandName, options => options.MapFrom(src => src.ProductBrand.Name))
            .ForMember(dist => dist.TypeName, options => options.MapFrom(src => src.ProductType.Name))
            .ForMember(dist => dist.ImageUrl, options => options.MapFrom<ImageResolver>());
=======
            .ForMember(dist => dist.ProductBrand, options => options.MapFrom(src => src.ProductBrand.Name))
            .ForMember(dist => dist.ProductType, options => options.MapFrom(src => src.ProductType.Name))
            .ForMember(dist => dist.PictureUrl, options => options.MapFrom<ImageResolver>());
>>>>>>> origin/Dev

        CreateMap<ProductType, TypeDto>();
        CreateMap<ProductBrand, BrandDto>();
    }
}
