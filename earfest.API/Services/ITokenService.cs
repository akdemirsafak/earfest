using earfest.API.Domain.Entities;
using earfest.API.Models.Auth;
using System.Security.Claims;

namespace earfest.API.Services;

public interface ITokenService
{
    Task<AppTokenResponse> CreateTokenAsync(AppUser appUser);
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    Task<AppTokenResponse> CreateTokenByRefreshToken(string refreshToken, string accessToken);
}
