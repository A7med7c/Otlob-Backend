<<<<<<< HEAD
﻿using DomainLayer.Exceptions;
using Shared.CutomResponses;
=======
using DomainLayer.Exceptions;
using Shared.CutomResponses;
using System.Text.Json;
>>>>>>> origin/Dev

namespace E_Commerce.Web.CustomMiddleWares;

public class CustomExceptionHandlerMiddleware(RequestDelegate Next, ILogger<CustomExceptionHandlerMiddleware> Logger)
{
    private readonly RequestDelegate next = Next;
    private readonly ILogger<CustomExceptionHandlerMiddleware> logger = Logger;
<<<<<<< HEAD
=======
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
>>>>>>> origin/Dev

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
<<<<<<< HEAD

=======
>>>>>>> origin/Dev
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
<<<<<<< HEAD
        var cutomResponse = new CustomError()
=======
        var customResponse = new CustomError()
>>>>>>> origin/Dev
        {
            Message = ex.Message
        };

        var statusCode = ex switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            UnAuthorizedException => StatusCodes.Status401Unauthorized,
<<<<<<< HEAD
            BadRequestException badrequestException => GetBadRequestException(badrequestException, cutomResponse),
=======
            BadRequestException badrequestException => GetBadRequestException(badrequestException, customResponse),
>>>>>>> origin/Dev
            _ => StatusCodes.Status500InternalServerError
        };

        context.Response.StatusCode = statusCode;
<<<<<<< HEAD
        cutomResponse.StatusCode = statusCode;

        await context.Response.WriteAsJsonAsync(cutomResponse);
    }
    private static int GetBadRequestException(BadRequestException badrequestException, CustomError cutomResponse)
    {
        cutomResponse.Errors = badrequestException.Errors;
=======
        customResponse.StatusCode = statusCode;

        await context.Response.WriteAsJsonAsync(customResponse, JsonOptions);
    }

    private static int GetBadRequestException(BadRequestException badrequestException, CustomError customResponse)
    {
        customResponse.Errors = badrequestException.Errors;
>>>>>>> origin/Dev
        return StatusCodes.Status400BadRequest;
    }

    private static async Task HandleNotFoundEndPointAsync(HttpContext context)
    {
<<<<<<< HEAD
        // handel not found endpoint
=======
>>>>>>> origin/Dev
        if (context.Response.StatusCode == StatusCodes.Status404NotFound)
        {
            var response = new CustomError()
            {
                StatusCode = StatusCodes.Status404NotFound,
                Message = $"endpoint {context.Request.Path} is not found"
            };
<<<<<<< HEAD
            await context.Response.WriteAsJsonAsync(response);
=======
            await context.Response.WriteAsJsonAsync(response, JsonOptions);
>>>>>>> origin/Dev
        }
    }
}
