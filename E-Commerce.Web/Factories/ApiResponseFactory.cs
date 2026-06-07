using Microsoft.AspNetCore.Mvc;
using Shared.CutomResponses;

namespace E_Commerce.Web.Factories;

public static class ApiResponseFactory
{
    public static IActionResult GenerateValidationResponse(ActionContext context)
    {
<<<<<<< HEAD
        var errors = context.ModelState
              .Where(model => model.Value.Errors.Any())
              .Select(model => new ValidationError()
              {
                  Field = model.Key,
                  Errors = model.Value.Errors.Select(e => e.ErrorMessage)
=======
        var invalidEntries = context.ModelState
              .Where(model => model.Value?.Errors.Any() == true)
              .ToList();

        var validationErrors = invalidEntries
              .Select(model => new ValidationError()
              {
                  Field = model.Key,
                  Errors = model.Value!.Errors.Select(e => e.ErrorMessage)
>>>>>>> origin/Dev
              });

        var response = new ValidationErrorResponse()
        {
<<<<<<< HEAD
            ValidationErrors = errors
=======
            Errors = invalidEntries
                .SelectMany(model => model.Value!.Errors.Select(e => e.ErrorMessage)),
            ValidationErrors = validationErrors
>>>>>>> origin/Dev
        };
        return new BadRequestObjectResult(response);
    }
}
