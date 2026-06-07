using DomainLayer.Models.SettingsModule;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SeviceAbstraction;
using Shared.DTOs.Email;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace ServiceImplementation;

public class NotificationsService(IOptions<EmailSettings> EmailOptions, IOptions<TwilioSettings> TwilioOptions) : INotificationsService
{
    private readonly EmailSettings emailSettings = EmailOptions.Value;
    private readonly TwilioSettings twilioSettings = TwilioOptions.Value;

    public async Task SendEmailAsync(EmailRequestDto emailRequestDto)
    {
        var email = new MimeMessage()
        {
            Sender = MailboxAddress.Parse(emailSettings.Email),
            Subject = emailRequestDto.Subject
        };
        email.To.Add(MailboxAddress.Parse(emailRequestDto.To));
        var builder = new BodyBuilder();

        if (emailRequestDto.Attachments is not null)
        {
            byte[] filesBytes;
            foreach (var file in emailRequestDto.Attachments)
            {
                if (file.Length > 0)
                {
                    using var ms = new MemoryStream();
                    await file.CopyToAsync(ms);
                    filesBytes = ms.ToArray();
                    builder.Attachments.Add(file.FileName, filesBytes, ContentType.Parse(file.ContentType));
                }
            }
        }
        builder.HtmlBody = emailRequestDto.Body;
        email.Body = builder.ToMessageBody();
        email.From.Add(new MailboxAddress(emailSettings.DisplayName, emailSettings.Email));
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(emailSettings.Host, emailSettings.Port, SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(emailSettings.Email, emailSettings.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }

    public Task<MessageResource> SendSmsAsync(SMSRequestDto smsDto)
    {
        TwilioClient.Init(twilioSettings.AccountSID, twilioSettings.AuthToken);
        var result = MessageResource.CreateAsync(
            body: smsDto.Body,
            from: new Twilio.Types.PhoneNumber(twilioSettings.TwilioPhoneNumber),
            to: smsDto.PhoneNumber);
        return result;
    }
}
