using Shared;
using Shared.DTOs.Product;

namespace SeviceAbstraction;

public interface IProductService
{
    Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams);
    Task<ProductDto> GetProductByIdAsync(int id);
    Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
    Task<IEnumerable<TypeDto>> GetAllTypesAsync();

}
