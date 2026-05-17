using Microsoft.AspNetCore.Mvc;
using SeviceAbstraction;
using Shared;
using Shared.Identity;

namespace PresentationLayer.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UserController(IServicesManager servicesManager) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var userDto = await servicesManager.AuthenticationService.LoginAsync(loginDto);
            return Ok(userDto);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var userDto = await servicesManager.AuthenticationService.RegisterAsync(registerDto);
            return Ok(userDto);
        }
    }
}
