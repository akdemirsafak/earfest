using earfest.Shared.Base;
using Earfest.Auth.Models.User;

namespace Earfest.Auth.AbstractServices;

public interface IUserService
{
    Task<AppResult<NoContentDto>> ChangePasswordAsync(ChangePasswordRequest request);
    Task<AppResult<List<UserResponse>>> GetUsersAsync();
    Task<AppResult<ProfileResponse>> ProfileAsync();
    Task<AppResult<UserResponse>> UpdateAsync(UpdateUserRequest request);
    Task<AppResult<NoContentDto>> LogoutAsync();
}
