using earfest.Shared.Base;
using Earfest.Auth.Models;
using Earfest.Auth.Models.Auth;

namespace Earfest.Auth.AbstractServices;

public interface IAuthService
{

    Task<AppResult<NoContentDto>> RegisterAsync(RegisterRequest request);
    Task<AppResult<AppTokenResponse>> LoginAsync(LoginRequest request);
    Task<AppResult<NoContentDto>> ConfirmEmailAsync(ConfirmEmailRequest request);
    Task<AppResult<NoContentDto>> ForgotPasswordAsync(ForgotPasswordRequest request);
    Task<AppResult<NoContentDto>> ResetPasswordAsync(string userId, string token, string password);
}
