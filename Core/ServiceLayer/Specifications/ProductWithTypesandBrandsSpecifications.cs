using DomainLayer.Models.Product;
using Shared;

namespace ServiceImplementation.Specifications;

internal class ProductWithTypesandBrandsSpecifications : BaseSpecifications<Product, int>
{
    public ProductWithTypesandBrandsSpecifications(ProductQueryParams queryParams) :
        base(p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId)
        && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
<<<<<<< HEAD
        && (string.IsNullOrWhiteSpace(queryParams.SearchPhrase) || p.Name.ToLower().Contains(queryParams.SearchPhrase) || p.Description.ToLower().Contains(queryParams.SearchPhrase)))
=======
        && (string.IsNullOrWhiteSpace(queryParams.Search) || p.Name.ToLower().Contains(queryParams.Search.ToLower()) || p.Description.ToLower().Contains(queryParams.Search.ToLower())))
>>>>>>> origin/Dev
    {
        AddIncludes(p => p.ProductType);
        AddIncludes(p => p.ProductBrand);

<<<<<<< HEAD
        switch (queryParams.sortingOptions)
=======
        switch (queryParams.SortingOptions)
>>>>>>> origin/Dev
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
