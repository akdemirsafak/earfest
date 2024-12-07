using earfest.Shared.Events;
using MassTransit;
using Notification.Service.Notifications;

namespace Notification.Service.Consumers;

public class ForgotPasswordEventConsumer : IConsumer<ForgotPasswordEvent>
{
    private readonly IEmailService _emailService;
    public ForgotPasswordEventConsumer(IEmailService emailService)
    {
        _emailService = emailService;
    }
    public async Task Consume(ConsumeContext<ForgotPasswordEvent> context)
    {
        var message = context.Message;
        await _emailService.SendEmailAsync(message.To, message.Subject, message.Body);
    }
}