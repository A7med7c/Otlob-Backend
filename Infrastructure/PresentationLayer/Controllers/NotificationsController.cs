using Microsoft.AspNetCore.Mvc;
using SeviceAbstraction;
using Shared.DTOs.Email;

namespace PresentationLayer.Controllers
{
    [Route("api/notifications")]
    public class NotificationsController(INotificationsService notificationsService) : ApiBaseController
    {
        [HttpPost("send-email")]
        public async Task<ActionResult> SendEmail([FromForm] EmailRequestDto emailRequestDto)
        {
            await notificationsService.SendEmailAsync(emailRequestDto);
            return Ok();
        }
        [HttpPost("send-sms")]
        public async Task<ActionResult> SendSMS(SMSRequestDto smsRequestDto)
        {
            var result = await notificationsService.SendSmsAsync(smsRequestDto);
            if (!string.IsNullOrEmpty(result.ErrorMessage))
                return BadRequest(result.ErrorMessage);
            return Ok(result);
        }
    }
}