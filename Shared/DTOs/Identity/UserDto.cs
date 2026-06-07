using System.Text.Json.Serialization;

namespace Shared.DTOs.Identity;

public class UserDto
{
    public string DisplayName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Token { get; set; } = default!;
    [JsonIgnore]
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiration { get; set; }
}
