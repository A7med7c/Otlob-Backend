namespace DomainLayer.Models.OrderModule;

public class ShippingAddress
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Country { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Street { get; set; } = default!;
}
