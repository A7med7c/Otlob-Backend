namespace DomainLayer.Exceptions;

public class BadRequestException(List<string> errors) : Exception("Validation Failed")
{
    public List<string> Errors { get; } = errors;
}
