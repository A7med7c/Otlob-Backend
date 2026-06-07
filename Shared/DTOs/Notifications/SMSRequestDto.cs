namespace Shared.DTOs.Email
{
    public class SMSRequestDto
    {
        public string PhoneNumber { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
