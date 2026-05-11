namespace DomainLayer.Exceptions;

public class BasketNotFoundException(string key) : NotFoundException($"Basket with id:{key} not found")
{
}
