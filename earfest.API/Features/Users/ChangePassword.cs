using earfest.API.Base;
using earfest.API.Domain.Entities;
using earfest.Shared.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace earfest.API.Features.Users;

public static class ChangePassword
{
    public record Command(string Email, string CurrentPassword, string NewPassword) : IRequest<AppResult<NoContentDto>>;
    public class CommandHandler : IRequestHandler<Command, AppResult<NoContentDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        public CommandHandler(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ISendEndpointProvider sendEndpointProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _sendEndpointProvider = sendEndpointProvider;
        }
        public async Task<AppResult<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return AppResult<NoContentDto>.Fail(new List<string> { "User not found" });
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);

                string emailBody = "Your password has been changed successfully.";

                var sendEndpoint=await _sendEndpointProvider.GetSendEndpoint(new System.Uri("queue:password-changed-email-queue"));
                var userCreatedEvent=new PasswordChangedEmailEvent { To = user.Email, Subject = "Reset Password", Body = emailBody };
                await sendEndpoint.Send<PasswordChangedEmailEvent>(userCreatedEvent);


                return AppResult<NoContentDto>.Success();
            }
            return AppResult<NoContentDto>.Fail(result.Errors.Select(x => x.Description).ToList());
        }
    }
}
