<<<<<<< HEAD
﻿using System.Net;
=======
using System.Net;
>>>>>>> origin/Dev

namespace Shared.CutomResponses;

public class ValidationErrorResponse
{
    public int StatusCode { get; set; } = (int)HttpStatusCode.BadRequest;
<<<<<<< HEAD
    public string Message { get; set; } = default!;
=======
    public string Message { get; set; } = "Validation failed";
    public IEnumerable<string> Errors { get; set; } = [];
>>>>>>> origin/Dev
    public IEnumerable<ValidationError> ValidationErrors { get; set; } = [];
}
