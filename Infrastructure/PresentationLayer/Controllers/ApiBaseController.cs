using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;


namespace PresentationLayer.Controllers
{
    [ApiController]
<<<<<<< HEAD
    [Route("api/[Controller]")]
=======
>>>>>>> origin/Dev
    public class ApiBaseController : ControllerBase
    {
        protected string GetCurrentUserEmail() => User.FindFirstValue(ClaimTypes.Email)!;
    }
}
