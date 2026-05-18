namespace DomainLayer.Exceptions
{
    public sealed class OrderNotFoundException(Guid id) : NotFoundException($"Order with id:{id} not found")
    {
    }
}
