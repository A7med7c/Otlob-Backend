using DomainLayer.Exceptions;
using Shared.CutomResponses;
using System.Text.Json;

namespace E_Commerce.Web.CustomMiddleWares;

public class CustomExceptionHandlerMiddleware(RequestDelegate Next, ILogger<CustomExceptionHandlerMiddleware> Logger)
{
    private readonly RequestDelegate next = Next;
    private readonly ILogger<CustomExceptionHandlerMiddleware> logger = Logger;
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

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
        var customResponse = new CustomError()
        {
            Message = ex.Message
        };

        var statusCode = ex switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            UnAuthorizedException => StatusCodes.Status401Unauthorized,
            BadRequestException badrequestException => GetBadRequestException(badrequestException, customResponse),
            _ => StatusCodes.Status500InternalServerError
        };

        context.Response.StatusCode = statusCode;
        customResponse.StatusCode = statusCode;

        await context.Response.WriteAsJsonAsync(customResponse, JsonOptions);
    }

    private static int GetBadRequestException(BadRequestException badrequestException, CustomError customResponse)
    {
        customResponse.Errors = badrequestException.Errors;
        return StatusCodes.Status400BadRequest;
    }

    private static async Task HandleNotFoundEndPointAsync(HttpContext context)
    {
        if (context.Response.StatusCode == StatusCodes.Status404NotFound)
        {
            var response = new CustomError()
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = $"endpoint {context.Request.Path} is not found"
            };
            await context.Response.WriteAsJsonAsync(response, JsonOptions);
        }
    }
}
