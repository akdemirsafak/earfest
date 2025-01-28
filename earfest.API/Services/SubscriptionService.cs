using earfest.API.Domain.DbContexts;
using earfest.API.Domain.Entities;
using earfest.API.Helpers;
using MassTransit;

namespace earfest.API.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly EarfestDbContext _dbContext;
    private readonly IPaymentService _paymentService;
    private readonly ICurrentUser _currentUser;

    private readonly ISendEndpointProvider _sendEndpointProvider;

    public SubscriptionService(EarfestDbContext dbContext,
        IPaymentService paymentService,
        ICurrentUser currentUser,
        ISendEndpointProvider sendEndpointProvider
    )
    {
        _dbContext = dbContext;
        _paymentService = paymentService;
        _currentUser = currentUser;
        _sendEndpointProvider = sendEndpointProvider;
    }

   

    public async Task RenewSubscription(string subscriptionId)
    {
        //UserSubscription? subscription = await _dbContext.UserSubscriptions.FindAsync(subscriptionId);
        //if (subscription == null || !subscription.IsActive)
        //    throw new Exception("Active subscription not found.");

        ////var user = await _dbContext.Users.FindAsync(subscription.UserId);


        //subscription.StartDate = subscription.EndDate;
        //subscription.EndDate = subscription.EndDate.AddMonths(1); // Varsayılan süreyi uzat

        //var paymentResult = await _paymentService.PayAsync(user.PaymentProviderCustomerId!,user.CardToken!,subscription.Plan.Price);
        //subscription.PaymentStatus = PaymentStatus.Success;

        //_dbContext.UserSubscriptions.Update(subscription);
        //await _dbContext.SaveChangesAsync();

    }

    //public async Task CancelSubscription(string subscriptionId)
    //{
    //    UserSubscription subscription = await _dbContext.UserSubscriptions.FindAsync(subscriptionId);
    //    if (subscription == null || !subscription.IsActive)
    //        throw new Exception("Active subscription not found");

    //    subscription.IsActive = false;
    //    _dbContext.UserSubscriptions.Update(subscription);
    //    await _dbContext.SaveChangesAsync();
    //}
}
