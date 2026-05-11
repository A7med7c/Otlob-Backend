using Shared.CutomResponses;

namespace E_Commerce.Web.CustomMiddleWares;

public class CustomExceptionHandlerMiddleware(RequestDelegate Next, ILogger<CustomExceptionHandlerMiddleware> Logger)
{
    private readonly RequestDelegate next = Next;
    private readonly ILogger<CustomExceptionHandlerMiddleware> logger = Logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Something Went Wrong");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            context.Response.ContentType = "application/json";

            var cutomResponse = new CustomError()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = ex.Message,
            };

            await context.Response.WriteAsJsonAsync(cutomResponse);

        }
    }
}
