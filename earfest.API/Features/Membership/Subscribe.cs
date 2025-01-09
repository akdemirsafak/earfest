using earfest.API.Base;
using earfest.API.Domain.DbContexts;
using earfest.API.Domain.Entities;
using earfest.API.Helpers;
using earfest.API.Models.Payment;
using earfest.API.Services;
using earfest.Shared.Events;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace earfest.API.Features.Membership;

public static class Subscribe
{
    public record Command(string PlanId, string CardHolderName, string CardNumber, int CVV, int expiredMonth, int expiredYear) : IRequest<AppResult<NoContentDto>>;
    public class CommandHandler : IRequestHandler<Command, AppResult<NoContentDto>>
    {
        private readonly EarfestDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICurrentUser _currentUser;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IPaymentService _paymentService;

        public CommandHandler(EarfestDbContext dbContext,
            UserManager<AppUser> userManager,
            ICurrentUser currentUser,
            ISendEndpointProvider sendEndpointProvider,
            IPaymentService paymentService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _currentUser = currentUser;
            _sendEndpointProvider = sendEndpointProvider;
            _paymentService = paymentService;

        }
        public async Task<AppResult<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var hasActiveSubscription = await _dbContext.UserSubscriptions.AnyAsync(x => x.UserId == _currentUser.GetUserId && x.IsActive);
            if (hasActiveSubscription)
                return AppResult<NoContentDto>.Fail("You already have an active subscription.");

            /////////////////////
            Plan plan = await _dbContext.Plans.FindAsync(request.PlanId); // Find olduğu için plan yoksa hata fırlatır.

            AppUser user = await _userManager.FindByIdAsync(_currentUser.GetUserId);

            // PAYMENT
            PayResponse paymentResult=await _paymentService.PayAsync(request.CardHolderName, request.CardNumber, request.CVV, request.expiredMonth, request.expiredYear, plan.Price);

            // SUBSCRIBE

            UserSubscription subscription = new UserSubscription
            {
                UserId = _currentUser.GetUserId,
                PlanId = plan.Id,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.Add(TimeSpan.FromDays(plan.Duration)),
                IsActive = true,
                PaymentStatus = PaymentStatus.Success
            };

            await _dbContext.UserSubscriptions.AddAsync(subscription);

            // ORDER
            var order=new Order
            {
                CreatedAt = DateTime.UtcNow,
                BuyerId = _currentUser.GetUserId,
                HolderName = request.CardHolderName,
                CardNumber = request.CardNumber.ToString(),
                Price = plan.Price,
                PlanId = plan.Id,
                PlanName = plan.Name
            };

            await _dbContext.Orders.AddAsync(order);

            user.CardToken = paymentResult.PaymentToken;
            user.PaymentProviderCustomerId= paymentResult.PaymentProviderCustomerId;

            await _dbContext.SaveChangesAsync();

            await _userManager.UpdateAsync(user);


            //SEND EMAIL 

            var sendEndpoint=await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:send-payment-purchased-email-queue"));

            var emailBody = $"Merhaba {user.FirstName} {user.LastName}, {plan.Name} planını başarıyla satın aldınız. Planınızı kullanmaya başlayabilirsiniz.";

            var paymentSuccessMailBody=new SubscribedEvent
            {
                To = user.Email!,
                Subject = "Plan Satın Alındı.",
                Body = emailBody
            };
            await sendEndpoint.Send(paymentSuccessMailBody);


            return AppResult<NoContentDto>.Success();
        }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.PlanId)
                .NotEmpty();
            
            RuleFor(x => x.CardHolderName)
                .NotEmpty()
                .MinimumLength(5);

            RuleFor(x => x.CardNumber)
                .NotEmpty()
                .Length(16);
            
            RuleFor(x => x.CVV)
                .NotEmpty()
                .LessThanOrEqualTo(999);
            
            RuleFor(x => x.expiredMonth)
                .NotNull()
                .InclusiveBetween(1, 12);
           
            RuleFor(x => x.expiredYear)
                .NotNull()
                .InclusiveBetween(DateTime.Now.Year, DateTime.Now.Year + 10);
        }
    }
}
