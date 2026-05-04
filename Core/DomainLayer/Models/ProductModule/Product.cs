namespace DomainLayer.Models.Product;

public class Product : BaseEntity<int>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string PictureUrl { get; set; } = null!;
    public decimal Price { get; set; }
    public ProductBrand ProductBrand { get; set; }
    public int BrandId { get; set; }
    public ProductType ProductType { get; set; }
    public int TypeId { get; set; }
}
