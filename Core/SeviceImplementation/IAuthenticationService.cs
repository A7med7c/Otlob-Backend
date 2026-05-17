using Shared;
using Shared.Identity;

namespace SeviceAbstraction;

public interface IAuthenticationService
{
    Task<UserDto> RegisterAsync(RegisterDto registerDto);
    Task<UserDto> LoginAsync(LoginDto loginDto);
}
