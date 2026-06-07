using Shared.DTOs.Identity;
using System.Text.Json.Serialization;

namespace Shared.DTOs.Order;

public class ReturnedOrderDto
{
    public Guid Id { get; set; }
    public string BuyerEmail { get; set; } = default!;
    public DateTimeOffset OrderDate { get; set; }
    public AddressDto ShipToAddress { get; set; } = default!;
    public string DeliveryMethod { get; set; } = default!;
    public decimal DeliveryCost { get; set; }
    public ICollection<OrderItemDto> Items { get; set; } = [];

    [JsonPropertyName("subtotal")]
    public decimal SubTotal { get; set; }

    public string Status { get; set; } = default!;
    public decimal Total { get; set; }
}
