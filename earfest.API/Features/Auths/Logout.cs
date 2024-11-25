using earfest.API.Base;
using earfest.API.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace earfest.API.Features.Auths;

public static class Logout
{
    public record Command : IRequest<AppResult<NoContentDto>>;
    public class CommandHandler : IRequestHandler<Command, AppResult<NoContentDto>>
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        public CommandHandler(
            SignInManager<AppUser> signInManager, 
            UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<AppResult<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();
            return AppResult<NoContentDto>.Success();
        }
    }
}
