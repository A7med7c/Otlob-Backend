using DomainLayer.Models.OrderModule;

namespace ServiceImplementation.Specifications.OrderModule
{
    internal class OrderSpecifications : BaseSpecifications<Order, Guid>
    {
        public OrderSpecifications(string email) : base(o => o.UserEmail == email)
        {
            AddIncludes(o => o.DeliveyMethod);
            AddIncludes(o => o.Items);
            AddOrderByDesc(o => o.OrderDate);
        }
        public OrderSpecifications(Guid id) : base(o => o.Id == id)
        {
            AddIncludes(o => o.DeliveyMethod);
            AddIncludes(o => o.Items);
        }
    }
}
