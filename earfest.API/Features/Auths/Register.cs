using earfest.API.Base;
using earfest.API.Domain.DbContexts;
using earfest.API.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace earfest.API.Features.Auths;

public static class Register
{
    public record Command(string Email, string Password, string? FirstName, string? LastName) : IRequest<AppResult<NoContentDto>>;
    public class CommandHandler : IRequestHandler<Command, AppResult<NoContentDto>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly EarfestDbContext _context;
        public CommandHandler(UserManager<AppUser> userManager, 
            EarfestDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<AppResult<NoContentDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var plan = await _context.Plans.FirstOrDefaultAsync(x=>x.IsFree==true && x.IsTrial==false);
            var user = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = request.Email,
                UserName = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Plan = plan
            };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
                return AppResult<NoContentDto>.Success();

            return AppResult<NoContentDto>.Fail(result.Errors.Select(x => x.Description).ToList());
        }
    }
}
