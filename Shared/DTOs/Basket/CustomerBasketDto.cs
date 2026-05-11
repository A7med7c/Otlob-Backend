namespace Shared.DTOs.Basket;

public class CustomerBasketDto
{
    public string Id { get; set; } = default!;

    public ICollection<BasketItemsDto> Items { get; set; } = [];
}
