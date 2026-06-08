using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.Product;
using ServiceImplementation.Specifications;
using SeviceAbstraction;
using Shared;
using Shared.DTOs.Product;
using Shared.Enums;

namespace ServiceImplementation;

public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper
, IFileStorageService fileStorageService) : IProductService
{
    public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
    {
        var repo = _unitOfWork.GetRepository<Product, int>();

        var specs = new ProductWithTypesandBrandsSpecifications(queryParams);
        var products = await repo.GetAllAsync(specs);

        var Data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
        var totalspecs = new TotalRecordsSpecifications(queryParams);
        var TotalRecords = await repo.CountAsync(totalspecs);
        return new PaginatedResult<ProductDto>(queryParams.PageIndex, queryParams.PageSize, TotalRecords, Data);
    }

    public async Task<ProductDto> GetProductByIdAsync(int id)
    {
        var specs = new ProductWithTypesandBrandsSpecifications(id);
        var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(specs)
                                ?? throw new ProductNotFoundException(id);

        var productDto = _mapper.Map<Product, ProductDto>(product);
        return productDto;

    }
    public async Task<int> CreateAsync(CreateProductDto dto)
    {
        var imageResult =
            await fileStorageService.UploadAsync(dto.Image, UploadFolder.Products);

        var product = _mapper.Map<Product>(dto);

        product.PictureUrl = imageResult.Url;

        await _unitOfWork.GetRepository<Product, int>().AddAsync(product);

        return product.Id;
    }

    public async Task UpdateAsync(int id, UpdateProductDto dto)
    {
        var repo = _unitOfWork.GetRepository<Product, int>();

        var product = await repo.GetByIdAsync(id)
            ?? throw new ProductNotFoundException(id);

        if (dto.Image is not null)
        {
            await fileStorageService.DeleteAsync(product.PictureUrl);

            var imageResult = await fileStorageService.UploadAsync(dto.Image, UploadFolder.Products);

            product.PictureUrl = imageResult.Url;
        }

        _mapper.Map(dto, product);

        repo.Update(product);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var repo = _unitOfWork.GetRepository<Product, int>();

        var product = await repo.GetByIdAsync(id)
           ?? throw new ProductNotFoundException(id);

        await fileStorageService.DeleteAsync(product.PictureUrl);

        repo.Remove(product);
        await _unitOfWork.SaveChangesAsync();
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
