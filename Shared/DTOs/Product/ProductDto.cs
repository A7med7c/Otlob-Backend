<<<<<<< HEAD
﻿namespace Shared.DTOs.Product;
=======
namespace Shared.DTOs.Product;
>>>>>>> origin/Dev

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
<<<<<<< HEAD
    public string ImageUrl { get; set; } = default!;
    public decimal Price { get; set; }
    public string BrandName { get; set; } = default!;
    public string TypeName { get; set; } = default!;
=======
    public string PictureUrl { get; set; } = default!;
    public decimal Price { get; set; }
    public string ProductBrand { get; set; } = default!;
    public string ProductType { get; set; } = default!;
>>>>>>> origin/Dev
}
