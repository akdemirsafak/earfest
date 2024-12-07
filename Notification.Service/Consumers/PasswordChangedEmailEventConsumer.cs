using earfest.Shared.Events;
using MassTransit;
using Notification.Service.Notifications;

namespace Notification.Service.Consumers;

public class PasswordChangedEmailEventConsumer : IConsumer<PasswordChangedEmailEvent>
{
    private readonly IEmailService _emailService;

    public PasswordChangedEmailEventConsumer(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Consume(ConsumeContext<PasswordChangedEmailEvent> context)
    {
        var message = context.Message;
        await _emailService.SendEmailAsync(message.To, message.Subject, message.Body);
    }
}
