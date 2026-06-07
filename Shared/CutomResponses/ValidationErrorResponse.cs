using System.Net;

namespace Shared.CutomResponses;

public class ValidationErrorResponse
{
    public int StatusCode { get; set; } = (int)HttpStatusCode.BadRequest;
    public string Message { get; set; } = "Validation failed";
    public IEnumerable<string> Errors { get; set; } = [];
    public IEnumerable<ValidationError> ValidationErrors { get; set; } = [];
}
