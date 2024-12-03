using earfest.API.Base;
using earfest.API.Domain.Entities;
using earfest.API.Models.User;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace earfest.API.Features.Users;

public static class GetUsers
{
    public record Query() : IRequest<AppResult<List<UserResponse>>>;
    public class QueryHandler(UserManager<AppUser> _userManager) : IRequestHandler<Query, AppResult<List<UserResponse>>>
    {
        public async Task<AppResult<List<UserResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users.ToListAsync();
            return AppResult<List<UserResponse>>.Success(users.Adapt<List<UserResponse>>());
        }
    }
}
