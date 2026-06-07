using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;


namespace PresentationLayer.Controllers
{
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected string GetCurrentUserEmail() => User.FindFirstValue(ClaimTypes.Email)!;
    }
}
