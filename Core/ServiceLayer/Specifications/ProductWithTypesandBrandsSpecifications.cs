using DomainLayer.Models.Product;
using Shared;

namespace ServiceImplementation.Specifications;

internal class ProductWithTypesandBrandsSpecifications : BaseSpecifications<Product, int>
{
    public ProductWithTypesandBrandsSpecifications(ProductQueryParams queryParams) :
        base(p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId)
        && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
        && (string.IsNullOrWhiteSpace(queryParams.SearchPhrase) || p.Name.ToLower().Contains(queryParams.SearchPhrase) || p.Description.ToLower().Contains(queryParams.SearchPhrase)))
    {
        AddIncludes(p => p.ProductType);
        AddIncludes(p => p.ProductBrand);

        switch (queryParams.sortingOptions)
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
