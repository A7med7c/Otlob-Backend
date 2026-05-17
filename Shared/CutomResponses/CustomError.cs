namespace Shared.CutomResponses;

public class CustomError
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = default!;
    public List<string>? Errors { get; set; }
}
