<<<<<<< HEAD
﻿using Shared.DTOs.Identity;
=======
using Shared.DTOs.Identity;
using System.Text.Json.Serialization;
>>>>>>> origin/Dev

namespace Shared.DTOs.Order;

public class ReturnedOrderDto
{
    public Guid Id { get; set; }
<<<<<<< HEAD
    public string UserEmail { get; set; } = default!;
    public DateTimeOffset OrderDate { get; set; }
    public string DeliveyMethod { get; set; } = default!;
    public AddressDto ShippingAddress { get; set; } = default!;
    public string OrderStatus { get; set; } = default!;
    public ICollection<OrderItemDto> Items { get; set; } = [];
    public decimal SubTotal { get; set; }
=======
    public string BuyerEmail { get; set; } = default!;
    public DateTimeOffset OrderDate { get; set; }
    public AddressDto ShipToAddress { get; set; } = default!;
    public string DeliveryMethod { get; set; } = default!;
    public decimal DeliveryCost { get; set; }
    public ICollection<OrderItemDto> Items { get; set; } = [];

    [JsonPropertyName("subtotal")]
    public decimal SubTotal { get; set; }

    public string Status { get; set; } = default!;
>>>>>>> origin/Dev
    public decimal Total { get; set; }
}
