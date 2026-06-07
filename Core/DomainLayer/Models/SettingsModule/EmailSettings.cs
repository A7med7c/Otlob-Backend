namespace DomainLayer.Models.SettingsModule;

using System.ComponentModel.DataAnnotations;

public class EmailSettings
{
    [Required]
    public string Email { get; set; } = default!;

    [Required]
    public string DisplayName { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;

    [Required]
    public string Host { get; set; } = default!;

    [Range(1, 65535)]
    public int Port { get; set; }
}
