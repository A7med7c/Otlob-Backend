using DomainLayer.Models.Product;
using Shared;

namespace ServiceImplementation.Specifications;

internal class TotalRecordsSpecifications : BaseSpecifications<Product, int>
{
    public TotalRecordsSpecifications(ProductQueryParams queryParams) : base(p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId)
        && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
<<<<<<< HEAD
        && (string.IsNullOrWhiteSpace(queryParams.SearchPhrase) || p.Name.ToLower().Contains(queryParams.SearchPhrase) || p.Description.ToLower().Contains(queryParams.SearchPhrase)))
=======
        && (string.IsNullOrWhiteSpace(queryParams.Search) || p.Name.ToLower().Contains(queryParams.Search.ToLower()) || p.Description.ToLower().Contains(queryParams.Search.ToLower())))
>>>>>>> origin/Dev
    {

    }
}
