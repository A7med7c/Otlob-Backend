using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;


namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ApiBaseController : ControllerBase
    {
        protected string GetCurrentUserEmail() => User.FindFirstValue(ClaimTypes.Email)!;
    }
}
