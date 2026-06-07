namespace DomainLayer.Models.BasketModule
{
    public class CustomerBasket
    {
        public string Id { get; set; } = default!;

        public ICollection<BasketItem> Items { get; set; } = [];
<<<<<<< HEAD
=======
        public string? ClientSecret { get; set; }

        public string? PaymentIntentId { get; set; }

        public int? DeliveryMethodId { get; set; }

        public decimal? ShippingPrice { get; set; }
>>>>>>> origin/Dev
    }
}
