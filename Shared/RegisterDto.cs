using System.ComponentModel.DataAnnotations;

namespace Shared;

public class RegisterDto
{
    public string DisplayName { get; set; } = default!;
    public string UserName { get; set; } = default!;
    [Phone]
    public string Phone { get; set; } = default!;
    [EmailAddress]
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}
