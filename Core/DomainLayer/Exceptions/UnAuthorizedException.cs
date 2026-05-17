namespace DomainLayer.Exceptions
{
    public class UnAuthorizedException(string message = "InValid Email or Password") : Exception(message)
    {
    }
}
