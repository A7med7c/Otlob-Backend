using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using SeviceAbstraction;
using Shared.DTOs;

namespace ServiceImplementation;

public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
{
    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync();
        var productsDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
        return productsDto;
    }
    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
        var productDto = _mapper.Map<Product, ProductDto>(product);
        return productDto;

    }

    public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
    {
        var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
        var brandsDto = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(brands);
        return brandsDto;
    }

    public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
    {
        var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
        var typesDto = _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(types);
        return typesDto;
    }
}
