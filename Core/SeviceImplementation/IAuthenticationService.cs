using Shared.DTOs.Identity;

namespace SeviceAbstraction;

public interface IAuthenticationService
{
    Task RegisterAsync(RegisterDto registerDto);
    Task<string> ConfirmEmailAsync(string email, string token);
    Task ResendConfirmationEmailAsync(string email);
    Task<UserDto> LoginAsync(LoginDto loginDto);
    Task<bool> CheckEmailAsync(string email);
    Task<UserDto> GetCurrentUserAsync(string email);
    Task<AddressDto> GetCurrentUserAddressAsync(string email);
    Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto addressDto);

}
