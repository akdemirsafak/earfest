using earfest.API.Domain.DbContexts;
using earfest.API.Domain.Entities;
using earfest.API.Services;
using Microsoft.EntityFrameworkCore;

namespace earfest.API.WorkerServices;

public class SubscriptionWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public SubscriptionWorker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<EarfestDbContext>();
            var subscriptionService = scope.ServiceProvider.GetRequiredService<SubscriptionService>();

            var dueSubscriptions = await context.UserSubscriptions
            .Where(s => s.IsActive && s.EndDate <= DateTime.UtcNow && s.PaymentStatus != PaymentStatus.Success)
            .ToListAsync();

            foreach (var subscription in dueSubscriptions)
            {
                try
                {
                    // Ödeme sağlayıcısıyla entegre ol ve başarılı olursa:
                    await subscriptionService.RenewSubscription(subscription.Id);
                }
                catch (Exception ex)
                {
                    // Hata yönetimi
                    subscription.PaymentStatus = PaymentStatus.Failed;
                    context.UserSubscriptions.Update(subscription);
                }
            }

            await context.SaveChangesAsync();
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken); // Günlük kontrol
        }
    }
}
