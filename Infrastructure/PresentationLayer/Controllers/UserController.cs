using Microsoft.AspNetCore.Authorization;
<<<<<<< HEAD
=======
using Microsoft.AspNetCore.Http;
>>>>>>> origin/Dev
using Microsoft.AspNetCore.Mvc;
using SeviceAbstraction;
using Shared.DTOs.Identity;
using System.Security.Claims;

namespace PresentationLayer.Controllers
{
<<<<<<< HEAD
    public class UserController(IServicesManager servicesManager) : ApiBaseController
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var userDto = await servicesManager.AuthenticationService.LoginAsync(loginDto);
=======
    [Route("api/auth")]
    public class UserController(IServicesManager servicesManager) : ApiBaseController
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
        {
            var userDto = await servicesManager.AuthenticationService.LoginAsync(loginDto);
            if (!string.IsNullOrEmpty(userDto.RefreshToken))
                SetRefreshTokenInCookie(userDto.RefreshToken, userDto.RefreshTokenExpiration);
>>>>>>> origin/Dev
            return Ok(userDto);
        }

        [HttpPost("register")]
<<<<<<< HEAD
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var userDto = await servicesManager.AuthenticationService.RegisterAsync(registerDto);
            return Ok(userDto);
        }

        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
=======
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            await servicesManager.AuthenticationService.RegisterAsync(registerDto);

            return Ok(new { Message = "Registration successful. Please check your email to verify your account." });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var result = await servicesManager.AuthenticationService.RefreshTokenAsync(refreshToken);
            if (result is null)
                return BadRequest(result);
            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token)
        {
            var result =
                await servicesManager.AuthenticationService
                    .ConfirmEmailAsync(email, token);

            return Ok(new { Message = result });
        }

        [HttpPost("resend-confirmation")]
        public async Task<IActionResult> ResendConfirmationEmail([FromQuery] string email)
        {
            await servicesManager.AuthenticationService.ResendConfirmationEmailAsync(email);

            return Ok(new { Message = "Verification email sent." });
        }

        [HttpGet("email-exists")]
        public async Task<ActionResult<bool>> CheckEmail([FromQuery] string email)
>>>>>>> origin/Dev
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
<<<<<<< HEAD
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto dto)
=======
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress([FromBody] AddressDto dto)
>>>>>>> origin/Dev
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var userAddress = await servicesManager.AuthenticationService.UpdateCurrentUserAddressAsync(email!, dto);
            return Ok(userAddress);
        }
<<<<<<< HEAD
=======

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> LogOut([FromBody] LogoutDto logoutDto)
        {
            var token = logoutDto.Token ?? Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(token))
                return BadRequest("Token is Required");

            var result = await servicesManager.AuthenticationService.LogoutAsync(token);
            Response.Cookies.Delete("refreshToken");
            return Ok(result);
        }

        private void SetRefreshTokenInCookie(string refreshToken, DateTime expiresOn)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = expiresOn
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
>>>>>>> origin/Dev
    }
}
