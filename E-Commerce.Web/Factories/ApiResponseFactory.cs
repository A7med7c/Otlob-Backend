using Microsoft.AspNetCore.Mvc;
using Shared.CutomResponses;

namespace E_Commerce.Web.Factories;

public static class ApiResponseFactory
{
    public static IActionResult GenerateValidationResponse(ActionContext context)
    {
        var invalidEntries = context.ModelState
              .Where(model => model.Value?.Errors.Any() == true)
              .ToList();

        var validationErrors = invalidEntries
              .Select(model => new ValidationError()
              {
                  Field = model.Key,
                  Errors = model.Value!.Errors.Select(e => e.ErrorMessage)
              });

        var response = new ValidationErrorResponse()
        {
            Errors = invalidEntries
                .SelectMany(model => model.Value!.Errors.Select(e => e.ErrorMessage)),
            ValidationErrors = validationErrors
        };
        return new BadRequestObjectResult(response);
    }
}
