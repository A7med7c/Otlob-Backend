using AutoMapper;
using DomainLayer.Models.Product;
using Shared.DTOs.Product;

namespace ServiceImplementation.MappingProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dist => dist.ProductBrand, options => options.MapFrom(src => src.ProductBrand.Name))
            .ForMember(dist => dist.ProductType, options => options.MapFrom(src => src.ProductType.Name))
            .ForMember(dist => dist.PictureUrl, options => options.MapFrom<ImageResolver>());
        CreateMap<CreateProductDto, Product>()
                   .ForMember(dest => dest.PictureUrl, opt => opt.Ignore());

        CreateMap<UpdateProductDto, Product>()
        .ForMember(dest => dest.PictureUrl, opt => opt.Ignore())
        .ForMember(dest => dest.ProductBrand, opt => opt.Ignore())
        .ForMember(dest => dest.ProductType, opt => opt.Ignore());

        CreateMap<ProductType, TypeDto>();
        CreateMap<ProductBrand, BrandDto>();
    }
}
