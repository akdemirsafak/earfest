namespace earfest.API.Services;

public interface ISubscriptionService
{

    //Task<UserSubscription> SubscribeAsync(string planId);
    //Task<UserSubscription> SubscribeAsync(Plan plan);
    Task RenewSubscription(string subscriptionId);
    //Task CancelSubscription(string subscriptionId);
}
