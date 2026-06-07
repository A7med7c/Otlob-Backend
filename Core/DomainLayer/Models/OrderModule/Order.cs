namespace DomainLayer.Models.OrderModule;

public class Order : BaseEntity<Guid>
{
    public Order()
    {

    }
    public Order(string userEmail, DeliveryMethod deliveyMethod, ShippingAddress shippingAddress, ICollection<OrderItem> items, decimal subTotal, string paymentIntentId)
    {
        UserEmail = userEmail;
        DeliveyMethod = deliveyMethod;
        ShippingAddress = shippingAddress;
        Items = items;
        SubTotal = subTotal;
        PaymentIntentId = paymentIntentId;
    }

    public string UserEmail { get; set; } = default!;
    public DeliveryMethod DeliveyMethod { get; set; } = default!;
    public ShippingAddress ShippingAddress { get; set; } = default!;
    public ICollection<OrderItem> Items { get; set; } = [];
    public decimal SubTotal { get; set; }

    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public int DeliveryMethodId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public decimal GetTotal => SubTotal + DeliveyMethod.Price;
    public string PaymentIntentId { get; set; }
}
