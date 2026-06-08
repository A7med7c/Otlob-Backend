using Microsoft.AspNetCore.Http;

namespace Shared.DTOs.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public int BrandId { get; set; }

        public int TypeId { get; set; }

        public IFormFile Image { get; set; } = null!;
    }
}
