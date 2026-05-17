namespace DomainLayer.Exceptions
{
    public sealed class DeliveryNotFoundException(int id) : NotFoundException($"Delivery with id:{id} not found")
    {
    }
}
