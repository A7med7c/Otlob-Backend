using Shared.DTOs.Identity;

namespace Shared.DTOs.Order;

public class OrderDto
{
    public int DeliveryMethodId { get; set; }
    public string BasketId { get; set; } = default!;
    public AddressDto? ShipToAddress { get; set; }
    public AddressDto? Address { get; set; }
}
