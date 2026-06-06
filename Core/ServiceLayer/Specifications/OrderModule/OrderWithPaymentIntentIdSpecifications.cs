using DomainLayer.Models.OrderModule;

namespace ServiceImplementation.Specifications.OrderModule
{
    internal class OrderWithPaymentIntentIdSpecifications : BaseSpecifications<Order, Guid>
    {
        public OrderWithPaymentIntentIdSpecifications(string paymentIntentId) : base(o => o.PaymentIntentId == paymentIntentId)
        {
        }
    }
}
