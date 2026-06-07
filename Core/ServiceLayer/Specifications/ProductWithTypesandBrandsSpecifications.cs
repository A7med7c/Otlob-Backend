using DomainLayer.Models.Product;
using Shared;

namespace ServiceImplementation.Specifications;

internal class ProductWithTypesandBrandsSpecifications : BaseSpecifications<Product, int>
{
    public ProductWithTypesandBrandsSpecifications(ProductQueryParams queryParams) :
        base(p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId)
        && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
        && (string.IsNullOrWhiteSpace(queryParams.Search) || p.Name.ToLower().Contains(queryParams.Search.ToLower()) || p.Description.ToLower().Contains(queryParams.Search.ToLower())))
    {
        AddIncludes(p => p.ProductType);
        AddIncludes(p => p.ProductBrand);

        switch (queryParams.SortingOptions)
        {
            case ProductSortingOptions.NameAsc:
                AddOrderBy(p => p.Name);
                break;
            case ProductSortingOptions.NameDesc:
                AddOrderByDesc(p => p.Name);
                break;
            case ProductSortingOptions.PriceAsc:
                AddOrderBy(p => p.Price);
                break;
            case ProductSortingOptions.PriceDesc:
                AddOrderByDesc(p => p.Price);
                break;
            default:
                break;
        }

        ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
    }
    public ProductWithTypesandBrandsSpecifications(int id) : base(p => p.Id == id)
    {
        AddIncludes(p => p.ProductType);
        AddIncludes(p => p.ProductBrand);
    }
}
