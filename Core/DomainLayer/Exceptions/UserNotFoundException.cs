namespace DomainLayer.Exceptions
{
    public class UserNotFoundException(string email) : NotFoundException($"User With {email} Not Found")
    {
    }
}
