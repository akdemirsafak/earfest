using earfest.API.Base;
using earfest.API.Domain.Entities;
using earfest.API.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Web;

namespace earfest.API.Features.Auths;

public static class ForgotPassword
{
    public record Command(string Email) : IRequest<AppResult<NoContentDto>>;
    public class CommandHandler : IRequestHandler<Command, AppResult<NoContentDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailService _emailService;
        public CommandHandler(UserManager<AppUser> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }
        public async Task<AppResult<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return AppResult<NoContentDto>.Fail(new List<string> { "User not found" });
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = $"https://localhost:7145/reset-password?email={user.Email}&token={HttpUtility.UrlEncode(token)}";
            var emailBody = $"<a href='{callbackUrl}'>Reset Password</a>";
            await _emailService.SendEmailAsync(user.Email, "Reset Password", emailBody);
            return AppResult<NoContentDto>.Success();
        }
    }
}
