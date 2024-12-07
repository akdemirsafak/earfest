using earfest.Shared.Events;
using MassTransit;
using Notification.Service.Notifications;

namespace Notification.Service.Consumers;

public class ConfirmEmailEventConsumer : IConsumer<ConfirmEmailEvent>
{

    private readonly IEmailService _emailService;
    public ConfirmEmailEventConsumer(IEmailService emailService)
    {
        _emailService = emailService;
    }
    public async Task Consume(ConsumeContext<ConfirmEmailEvent> context)
    {
        var message = context.Message;
        await _emailService.SendEmailAsync(message.To, message.Subject, message.Body);
    }
}
