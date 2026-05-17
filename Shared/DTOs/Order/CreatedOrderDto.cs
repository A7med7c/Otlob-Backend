using Shared.DTOs.Identity;

namespace Shared.DTOs.Order;

public class CreatedOrderDto
{
    public Guid Id { get; set; }
    public string UserEmail { get; set; } = default!;
    public DateTimeOffset OrderDate { get; set; }
    public string DeliveyMethod { get; set; } = default!;
    public AddressDto ShippingAddress { get; set; } = default!;
    public string OrderStatus { get; set; } = default!;
    public ICollection<OrderItemDto> Items { get; set; } = [];
    public decimal SubTotal { get; set; }
    public decimal Total { get; set; }
}
