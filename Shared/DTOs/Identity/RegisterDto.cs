<<<<<<< HEAD
﻿using System.ComponentModel.DataAnnotations;
=======
using System.ComponentModel.DataAnnotations;
>>>>>>> origin/Dev

namespace Shared.DTOs.Identity;

public class RegisterDto
{
    public string DisplayName { get; set; } = default!;
<<<<<<< HEAD
    public string UserName { get; set; } = default!;
    [Phone]
    public string Phone { get; set; } = default!;
=======
    public string? UserName { get; set; }

    [Phone]
    public string? Phone { get; set; }

>>>>>>> origin/Dev
    [EmailAddress]
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}
