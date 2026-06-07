using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Basket;

public class BasketItemsDto
{
    public int Id { get; set; }
    public string ProductName { get; set; } = default!;
    public string PictureUrl { get; set; } = default!;
    public string Brand { get; set; } = default!;
    public string Type { get; set; } = default!;

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    [Range(0, 100)]
    public int Quantity { get; set; }
}
