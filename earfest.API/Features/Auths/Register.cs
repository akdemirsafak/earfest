using earfest.API.Base;
using earfest.API.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace earfest.API.Features.Auths;

public static class Register
{
    public record Command(string Email, string Password, string FirstName, string LastName) : IRequest<AppResult<NoContentDto>>;
    public class CommandHandler : IRequestHandler<Command, AppResult<NoContentDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        //private readonly IJwtGenerator _jwtGenerator;
        public CommandHandler(UserManager<AppUser> userManager)//, IJwtGenerator jwtGenerator
        {
            _userManager = userManager;
            //_jwtGenerator = jwtGenerator;
        }
        public async Task<AppResult<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = new AppUser
            {
                Email = request.Email,
                UserName = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
                return AppResult<NoContentDto>.Success();

            return AppResult<NoContentDto>.Fail(result.Errors.Select(x => x.Description).ToList());
        }
    }
}
