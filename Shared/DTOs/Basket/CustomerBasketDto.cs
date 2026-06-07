<<<<<<< HEAD
﻿namespace Shared.DTOs.Basket;
=======
namespace Shared.DTOs.Basket;
>>>>>>> origin/Dev

public class CustomerBasketDto
{
    public string Id { get; set; } = default!;
<<<<<<< HEAD

    public ICollection<BasketItemsDto> Items { get; set; } = [];
=======
    public ICollection<BasketItemsDto> Items { get; set; } = [];
    public string? ClientSecret { get; set; }
    public string? PaymentIntentId { get; set; }
    public int? DeliveryMethodId { get; set; }
    public decimal? ShippingPrice { get; set; }
>>>>>>> origin/Dev
}
