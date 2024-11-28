using earfest.API.Base;
using earfest.API.Domain.Entities;
using earfest.API.Helpers;
using earfest.API.Models.User;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace earfest.API.Features.Users;

public static class UserProfile
{
    public record Query() : IRequest<AppResult<UserResponse>>;
    public class QueryHandler(UserManager<AppUser> _userManager,
        ICurrentUser _currentUser): IRequestHandler<Query, AppResult<UserResponse>>
    {
        public async Task<AppResult<UserResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var users = await _userManager.FindByIdAsync(_currentUser.GetUserId);
            return AppResult<UserResponse>.Success(users.Adapt<UserResponse>());
        }
    }
}
