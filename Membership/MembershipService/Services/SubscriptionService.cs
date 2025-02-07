using System.Net.Http.Headers;
using earfest.Shared.Base;
using earfest.Shared.Events;
using earfest.Shared.Helpers;
using Mapster;
using MassTransit;
using MembershipService.DbContexts;
using MembershipService.Entities;
using MembershipService.Models.Payment;
using MembershipService.Models.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace MembershipService.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly MembershipDbContext _dbContext;
    private readonly ICurrentUser _currentUser;
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISendEndpointProvider _sendEndpointProvider;


    public SubscriptionService(MembershipDbContext dbContext,
        ICurrentUser currentUser,
        HttpClient httpClient,
        IHttpContextAccessor httpContextAccessor,
        ISendEndpointProvider sendEndpointProvider)
    {
        _dbContext = dbContext;
        _currentUser = currentUser;
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
        _sendEndpointProvider = sendEndpointProvider;
    }
    public async Task<AppResult<NoContentDto>> CreateAsync(SubscribeRequest request)
    {
        var hasSubscription = await _dbContext.Subscriptions.AnyAsync(x => x.UserId == _currentUser.GetUserId && x.Statu == SubscriptionStatu.Active);
        if (hasSubscription)
            throw new Exception("User already has a subscription");

        var plan = await _dbContext.Plans.FindAsync(request.PlanId);


        //Payment 
        var paymentDto = request.Adapt<PaymentDto>();

        var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Trim();

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var paymentResponse = await _httpClient.PostAsJsonAsync("https://localhost:7014/api/payment/pay", paymentDto);
        var paymentResult = await paymentResponse.Content.ReadFromJsonAsync<AppResult<PaymentServiceResponseDto>>(); //STATUS CODE 0 GELIYOR
        int statusCode = (int)paymentResponse.StatusCode;///////////////// StatusCode 0 gelmesi problemini burada çözümledim.

        if (statusCode != 201)
            return AppResult<NoContentDto>.Fail("Ödeme başarısız oldu", 400);


        //Payment completed Subscribe user.

        var subscription = new Subscription
        {
            UserId = _currentUser.GetUserId,
            PlanId = request.PlanId,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(1),
            Statu = SubscriptionStatu.Active,
            PaymentId = paymentResult.Data.PaymentId
        };

        await _dbContext.Subscriptions.AddAsync(subscription);
        await _dbContext.SaveChangesAsync();

        var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:send-subscription-email-queue"));
        await endpoint.Send(new SubscribedEvent
        {
            To = _currentUser.GetEmail,
            Subject = "Üyelik başlatıldı.",
            Body = $"Üyeliğiniz {DateTime.Now.ToString("dd/MM/yyyy")} tarihinde başlatıldı. {plan.Name} planının tüm avantajlarından faydalanacaksınız.!"
        });


        return AppResult<NoContentDto>.Success();

    }

    public async Task<AppResult<NoContentDto>> CancelAsync()
    {
        var hasSubscription = await _dbContext.Subscriptions
            .FirstOrDefaultAsync(x => x.UserId == _currentUser.GetUserId && x.Statu == SubscriptionStatu.Active);

        if (hasSubscription == null)
            throw new Exception("User does not have a subscription");

        hasSubscription.EndDate = DateTime.UtcNow;
        hasSubscription.Statu = SubscriptionStatu.Canceled;
        _dbContext.Subscriptions.Update(hasSubscription);
        await _dbContext.SaveChangesAsync();

        var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:send-unsubscription-email-queue"));

        await endpoint.Send(new UnSubscribedEvent
        {
            To = _currentUser.GetEmail,
            Subject = "Üyelik İptal Edildi.",
            Body = $"Üyeliğiniz {DateTime.Now.ToString("dd/MM/yyyy")} tarihinde iptal edildi. Seni özleyeceğiz..."
        });

        return AppResult<NoContentDto>.Success();
    }

    public async Task<AppResult<List<SubscriptionResponse>>> GetAsync()
    {
        var subscriptions = await _dbContext.Subscriptions.ToListAsync();
        var response = subscriptions.Adapt<List<SubscriptionResponse>>();
        return AppResult<List<SubscriptionResponse>>.Success(response, 200);

    }

}
