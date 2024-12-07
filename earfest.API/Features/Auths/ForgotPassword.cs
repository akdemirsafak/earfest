using earfest.API.Base;
using earfest.API.Domain.Entities;
using earfest.Shared.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading.Channels;
using System.Web;

namespace earfest.API.Features.Auths;

public static class ForgotPassword
{
    public record Command(string Email) : IRequest<AppResult<NoContentDto>>;
    public class CommandHandler : IRequestHandler<Command, AppResult<NoContentDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        public CommandHandler(UserManager<AppUser> userManager, 
            ISendEndpointProvider sendEndpointProvider)
        {
            _userManager = userManager;
            _sendEndpointProvider = sendEndpointProvider;
        }
        public async Task<AppResult<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return AppResult<NoContentDto>.Fail(new List<string> { "User not found" });
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = $"https://localhost:7145/api/auth/reset-password?userId={user.Id}&token={HttpUtility.UrlEncode(token)}";
            var emailBody = $"<a href='{callbackUrl}'>Reset Password</a>";
            // await _emailService.SendEmailAsync(user.Email, "Reset Password", emailBody);

            //await _sendEndpointProvider.Send(new ForgotPasswordEvent { To = user.Email!, Subject = "Reset Password", Body = emailBody });



            var sendEndpoint=await _sendEndpointProvider.GetSendEndpoint(new System.Uri("queue:forgot-password-email-queue"));

            var userCreatedEvent=new ForgotPasswordEvent { To = user.Email, Subject = "Reset Password", Body = emailBody };
            await sendEndpoint.Send<ForgotPasswordEvent>(userCreatedEvent);



            return AppResult<NoContentDto>.Success();
        }
    }
}
