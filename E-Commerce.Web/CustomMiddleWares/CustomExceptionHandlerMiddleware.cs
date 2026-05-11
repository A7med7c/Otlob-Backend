using DomainLayer.Exceptions;
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
            await HandleNotFoundEndPointAsync(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Something Went Wrong");

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.StatusCode = ex switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        context.Response.ContentType = "application/json";

        var cutomResponse = new CustomError()
        {
            StatusCode = context.Response.StatusCode,
            Message = ex.Message,
        };

        await context.Response.WriteAsJsonAsync(cutomResponse);
    }

    private static async Task HandleNotFoundEndPointAsync(HttpContext context)
    {
        // handel not found endpoint
        if (context.Response.StatusCode == StatusCodes.Status404NotFound)
        {
            var response = new CustomError()
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = $"endpoint {context.Request.Path} is not found"
            };
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
