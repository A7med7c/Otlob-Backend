namespace Shared;

public class ProductQueryParams
{
    private const int DefaultPageSize = 5;
    private const int MaxPageSize = 10;

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

    public int PageSize
    {
        get { return pageSize; }
        set { pageSize = value <= 0 || value > MaxPageSize ? DefaultPageSize : value; }
    }
}
