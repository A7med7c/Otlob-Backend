<<<<<<< HEAD
﻿namespace Shared;
=======
namespace Shared;
>>>>>>> origin/Dev

public class ProductQueryParams
{
    private const int DefaultPageSize = 5;
    private const int MaxPageSize = 10;
<<<<<<< HEAD
    public int? BrandId { get; set; }
    public int? TypeId { get; set; }
    public string? SearchPhrase { get; set; }
    public ProductSortingOptions sortingOptions { get; set; }

    public int PageIndex { get; set; }
    private int pageSize;
=======

    public int? BrandId { get; set; }
    public int? TypeId { get; set; }
    public string? Search { get; set; }
    public ProductSortingOptions SortingOptions { get; private set; } = ProductSortingOptions.NameAsc;

    public int PageIndex { get; set; } = 1;
    private int pageSize = DefaultPageSize;

    public string? Sort
    {
        get => SortingOptions.ToString();
        set => SortingOptions = value?.Trim().ToLowerInvariant() switch
        {
            null or "" or "nameasc" or "name" => ProductSortingOptions.NameAsc,
            "namedesc" => ProductSortingOptions.NameDesc,
            "priceasc" => ProductSortingOptions.PriceAsc,
            "pricedesc" => ProductSortingOptions.PriceDesc,
            _ => ProductSortingOptions.NameAsc
        };
    }
>>>>>>> origin/Dev

    public int PageSize
    {
        get { return pageSize; }
<<<<<<< HEAD
        set { pageSize = value > MaxPageSize ? DefaultPageSize : value; }
    }

=======
        set { pageSize = value <= 0 || value > MaxPageSize ? DefaultPageSize : value; }
    }
>>>>>>> origin/Dev
}
