using earfest.API.Base;
using earfest.API.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace earfest.API.Features.Auths;



//Forget Password ile şifremi unuttum ile buraya gelecek.
public static class ResetPassword
{
    public record Command(string Email, string Token, string Password) : IRequest<AppResult<NoContentDto>>;
    public class CommandHandler : IRequestHandler<Command, AppResult<NoContentDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        public CommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<AppResult<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return AppResult<NoContentDto>.Fail(new List<string> { "User not found" });
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (result.Succeeded)
                return AppResult<NoContentDto>.Success();
            return AppResult<NoContentDto>.Fail(result.Errors.Select(x => x.Description).ToList());
        }
    }
}
