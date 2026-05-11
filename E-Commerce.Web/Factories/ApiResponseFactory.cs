using Microsoft.AspNetCore.Mvc;
using Shared.CutomResponses;

namespace E_Commerce.Web.Factories;

public static class ApiResponseFactory
{
    public static IActionResult GenerateValidationResponse(ActionContext context)
    {
        var errors = context.ModelState
              .Where(model => model.Value.Errors.Any())
              .Select(model => new ValidationError()
              {
                  Field = model.Key,
                  Errors = model.Value.Errors.Select(e => e.ErrorMessage)
              });

        var response = new ValidationErrorResponse()
        {
            ValidationErrors = errors
        };
        return new BadRequestObjectResult(response);
    }
}
