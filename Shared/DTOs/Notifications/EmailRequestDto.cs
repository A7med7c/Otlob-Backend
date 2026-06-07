using Microsoft.AspNetCore.Http;

namespace Shared.DTOs.Email
{
    public class EmailRequestDto
    {

        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public List<IFormFile>? Attachments { get; set; }
    }
}
