namespace DomainLayer.Exceptions;

public sealed class UserAddressNotFoundException(string userName) : NotFoundException($"Address for username {userName} not found")
{
}
