using Microsoft.AspNetCore.Identity;

namespace DomainLayer.Models.IdetityModule;

public class ApplicationUser : IdentityUser
{
    public string DisplayName { get; set; } = default!;
    public Address? Address { get; set; }
}
