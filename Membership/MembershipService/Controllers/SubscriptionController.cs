using earfest.Shared.Base;
using MembershipService.Models.Subscriptions;
using MembershipService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MembershipService.Controllers;

[Authorize]
public class SubscriptionController(ISubscriptionService _subscriptionService) : EarfestBaseController
{
    [HttpPost]
    public async Task<IActionResult> Subscribe([FromBody] SubscribeRequest request)
    {
        var result = await _subscriptionService.CreateAsync(request);
        return CreateActionResult(result);
    }
    [HttpPost("cancel")]
    public async Task<IActionResult> Cancel()
    {
        var result = await _subscriptionService.CancelAsync();
        return CreateActionResult(result);
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _subscriptionService.GetAsync();
        return CreateActionResult(result);
    }
}
