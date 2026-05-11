using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.Product;
using ServiceImplementation.Specifications;
using SeviceAbstraction;
using Shared;
using Shared.DTOs;

namespace ServiceImplementation;

public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper) : IProductService
{
    public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
    {
        var repo = _unitOfWork.GetRepository<Product, int>();

        var specs = new ProductWithTypesandBrandsSpecifications(queryParams);
        var products = await repo.GetAllAsync(specs);

        var Data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
        var ProductsCount = Data.Count();

        var totalspecs = new TotalRecordsSpecifications(queryParams);
        var TotalRecords = await repo.CountAsync(totalspecs);
        return new PaginatedResult<ProductDto>(queryParams.PageIndex, ProductsCount, TotalRecords, Data);
    }
    public async Task<ProductDto?> GetProductByIdAsync(int id)
    {
        var specs = new ProductWithTypesandBrandsSpecifications(id);
        var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(specs);
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
