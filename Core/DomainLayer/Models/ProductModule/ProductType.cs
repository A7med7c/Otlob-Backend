namespace DomainLayer.Models.Product;

public class ProductType : BaseEntity<int>
{
    public string Name { get; set; } = default!;
}
