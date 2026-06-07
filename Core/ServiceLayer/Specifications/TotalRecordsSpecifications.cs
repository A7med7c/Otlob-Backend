using DomainLayer.Models.Product;
using Shared;

namespace ServiceImplementation.Specifications;

internal class TotalRecordsSpecifications : BaseSpecifications<Product, int>
{
    public TotalRecordsSpecifications(ProductQueryParams queryParams) : base(p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId)
        && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
        && (string.IsNullOrWhiteSpace(queryParams.Search) || p.Name.ToLower().Contains(queryParams.Search.ToLower()) || p.Description.ToLower().Contains(queryParams.Search.ToLower())))
    {

    }
}
