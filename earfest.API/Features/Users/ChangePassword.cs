using earfest.API.Base;
using earfest.API.Domain.Entities;
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
        public CommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                return AppResult<NoContentDto>.Success();
            }
            return AppResult<NoContentDto>.Fail(result.Errors.Select(x => x.Description).ToList());
        }
    }
}
