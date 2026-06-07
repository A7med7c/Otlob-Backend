using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs.Identity;

public class RegisterDto
{
    public string DisplayName { get; set; } = default!;
    public string? UserName { get; set; }

    [Phone]
    public string? Phone { get; set; }

    [EmailAddress]
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}
