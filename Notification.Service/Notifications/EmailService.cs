using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Notification.Service.Notifications;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    public EmailService(IOptions<EmailSettings> settings)
    {
        _emailSettings = settings.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string body)
    {
        var smtpClient = new SmtpClient();
        smtpClient.Host = _emailSettings.Host;
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.EnableSsl = true;
        smtpClient.Port = 587;
        smtpClient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);


        var mailMessage = new MailMessage
        {
            From = new MailAddress(_emailSettings.Email),
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
        };
        mailMessage.To.Add(email);

        await smtpClient.SendMailAsync(mailMessage);
    }
}
