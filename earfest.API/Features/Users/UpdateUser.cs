using earfest.API.Base;
using earfest.API.Domain.Entities;
using earfest.API.Helpers;
using earfest.API.Models.User;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace earfest.API.Features.Users;

public static class UpdateUser
{
    public record Command(string FirstName, string LastName, DateTime? BirthDate) : IRequest<AppResult<UserResponse>>;
    public class CommandHandler : IRequestHandler<Command, AppResult<UserResponse>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICurrentUser _currentUser;

        public CommandHandler(UserManager<AppUser> userManager, ICurrentUser currentUser)
        {
            _userManager = userManager;
            _currentUser = currentUser;
        }

        public async Task<AppResult<UserResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(_currentUser.GetUserId);

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.BirthDate = request.BirthDate;

            await _userManager.UpdateAsync(user);
            return AppResult<UserResponse>.Success(user.Adapt<UserResponse>(), 200);
        }
    }
}
