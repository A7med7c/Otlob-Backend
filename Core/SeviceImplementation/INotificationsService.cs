using Shared.DTOs.Email;
using Twilio.Rest.Api.V2010.Account;

namespace SeviceAbstraction;

public interface INotificationsService
{
    Task SendEmailAsync(EmailRequestDto emailRequestDto);
    Task<MessageResource> SendSmsAsync(SMSRequestDto smsDto);
}
