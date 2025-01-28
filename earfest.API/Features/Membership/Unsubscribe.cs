using earfest.API.Domain.DbContexts;
using earfest.API.Domain.Entities;
using earfest.API.Helpers;
using earfest.Shared.Base;
using earfest.Shared.Events;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace earfest.API.Features.Membership;

public static class Unsubscribe
{
    public record Command(string Id) : IRequest<AppResult<NoContentDto>>;
    public class CommandHandler : IRequestHandler<Command, AppResult<NoContentDto>>
    {
        private readonly EarfestDbContext _dbContext;
        private readonly ICurrentUser _currentUser;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public CommandHandler(EarfestDbContext dbContext, 
            ICurrentUser currentUser, 
            ISendEndpointProvider sendEndpointProvider)
        {
            _dbContext = dbContext;
            _currentUser = currentUser;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task<AppResult<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {


            //UserSubscription subscription = await _dbContext.UserSubscriptions
            //    .Include(x=>x.User)
            //    .Include(x=>x.Plan)
            //    .SingleOrDefaultAsync(x=>x.Id==request.Id && x.UserId==_currentUser.GetUserId);
            //if (subscription == null || !subscription.IsActive)
            //    return AppResult<NoContentDto>.Fail("Active subscription not found");

            //subscription.IsActive = false;
            //_dbContext.UserSubscriptions.Update(subscription);
            //await _dbContext.SaveChangesAsync();


            //var sendEndpoint=await _sendEndpointProvider.GetSendEndpoint(new System.Uri("queue:send-unsubscribed-email-queue"));

            //var emailBody= $"Merhaba {subscription.User.FirstName} {subscription.User.LastName}, {subscription.Plan.Name} planınız başarıyla iptal edilmiştir.";

            //var unsubsribedMailBody=new UnSubscribedEvent
            //{
            //    To = subscription.User.Email,
            //    Subject = "Plan İptal Edildi.",
            //    Body = emailBody
            //};


            //await sendEndpoint.Send<UnSubscribedEvent>(unsubsribedMailBody);

            return AppResult<NoContentDto>.Success();
        }
    }
}
