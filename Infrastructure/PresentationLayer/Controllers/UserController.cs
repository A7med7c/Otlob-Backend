using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeviceAbstraction;
using Shared.DTOs.Identity;
using System.Security.Claims;

namespace PresentationLayer.Controllers
{
    public class UserController(IServicesManager servicesManager) : ApiBaseController
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

        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var isExisted = await servicesManager.AuthenticationService.CheckEmailAsync(email);
            return Ok(isExisted);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = GetCurrentUserEmail();
            var user = await servicesManager.AuthenticationService.GetCurrentUserAsync(email!);
            return Ok(user);
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var userAddress = await servicesManager.AuthenticationService.GetCurrentUserAddressAsync(email!);
            return Ok(userAddress);
        }

        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto dto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var userAddress = await servicesManager.AuthenticationService.UpdateCurrentUserAddressAsync(email!, dto);
            return Ok(userAddress);
        }
    }
}
