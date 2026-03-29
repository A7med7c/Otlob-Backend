namespace DomainLayer.Models;

public class ProductType : BaseEntity<int>
{
    public string Name { get; set; } = default!;
}
