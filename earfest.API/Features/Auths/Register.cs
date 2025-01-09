using earfest.API.Base;
using earfest.API.Domain.DbContexts;
using earfest.API.Domain.Entities;
using earfest.Shared.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Web;

namespace earfest.API.Features.Auths;

public static class Register
{
    public record Command(string Email, string Password, string? FirstName, string? LastName) : IRequest<AppResult<NoContentDto>>;
    public class CommandHandler : IRequestHandler<Command, AppResult<NoContentDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly EarfestDbContext _context;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        public CommandHandler(UserManager<AppUser> userManager,
            EarfestDbContext context,
            ISendEndpointProvider sendEndpointProvider)
        {
            _userManager = userManager;
            _context = context;
            _sendEndpointProvider = sendEndpointProvider;
        }
        public async Task<AppResult<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = request.Email,
                UserName = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                CreatedAt = DateTime.UtcNow
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            var callbackUrl = $"https://localhost:7145/api/auth/confirm-email?userId={user.Id}&token={HttpUtility.UrlEncode(await _userManager.GenerateEmailConfirmationTokenAsync(user))}";
            var emailBody = $"<a href='{callbackUrl}'>Confirm Email</a>";

            var sendEndpoint=await _sendEndpointProvider.GetSendEndpoint(new System.Uri("queue:send-confirm-email-queue"));

            var userCreatedEvent=new ConfirmEmailEvent { To = user.Email, Subject = "Confirm Email", Body = emailBody };
            await sendEndpoint.Send<ConfirmEmailEvent>(userCreatedEvent);


            if (result.Succeeded)
                return AppResult<NoContentDto>.Success();

            return AppResult<NoContentDto>.Fail(result.Errors.Select(x => x.Description).ToList());
        }
    }
}
