namespace Notification.Service.Notifications;

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string body);
}
