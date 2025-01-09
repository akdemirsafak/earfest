using earfest.Shared.Events;
using MassTransit;
using Notification.Service.Notifications;

namespace Notification.Service.Consumers;

public class SubscribedEventConsumer : IConsumer<SubscribedEvent>
{
    private readonly IEmailService _emailService;

    public SubscribedEventConsumer(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Consume(ConsumeContext<SubscribedEvent> context)
    {
        var planPurchasedEvent = context.Message;
        await _emailService.SendEmailAsync(planPurchasedEvent.To, planPurchasedEvent.Subject, planPurchasedEvent.Body);
    }
}
