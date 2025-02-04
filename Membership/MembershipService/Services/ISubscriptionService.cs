using earfest.Shared.Base;
using MembershipService.Models.Subscriptions;

namespace MembershipService.Services;

public interface ISubscriptionService
{
    Task<AppResult<NoContentDto>> CreateAsync(SubscribeRequest request);
    Task<AppResult<NoContentDto>> CancelAsync();
    Task<AppResult<List<SubscriptionResponse>>> GetAsync();
}
