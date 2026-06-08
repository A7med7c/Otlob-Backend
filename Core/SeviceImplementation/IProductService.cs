using Shared;
using Shared.DTOs.Product;

namespace SeviceAbstraction;

public interface IProductService
{
    Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams);
    Task<ProductDto> GetProductByIdAsync(int id);
    Task<int> CreateAsync(CreateProductDto dto);
    Task UpdateAsync(int id, UpdateProductDto dto);
    Task DeleteAsync(int id);
    Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
    Task<IEnumerable<TypeDto>> GetAllTypesAsync();

}
