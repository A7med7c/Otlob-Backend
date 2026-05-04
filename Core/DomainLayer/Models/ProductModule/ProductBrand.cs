namespace DomainLayer.Models.Product;

public class ProductBrand : BaseEntity<int>
{
    public string Name { get; set; } = default!;
}
