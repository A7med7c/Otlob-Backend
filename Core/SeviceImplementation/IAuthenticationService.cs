using Shared.DTOs.Identity;

namespace SeviceAbstraction;

public interface IAuthenticationService
{
    Task<UserDto> RegisterAsync(RegisterDto registerDto);
    Task<UserDto> LoginAsync(LoginDto loginDto);
    Task<bool> CheckEmailAsync(string email);

    Task<UserDto> GetCurrentUserAsync(string email);
    Task<AddressDto> GetCurrentUserAddressAsync(string email);
    Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto addressDto);

}
