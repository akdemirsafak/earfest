using System.Security.Claims;
using earfest.Shared.Models;
using Earfest.Auth.Entities;

namespace Earfest.Auth.AbstractServices;

public interface ITokenService
{
    Task<AppTokenResponse> CreateTokenAsync(AppUser appUser);
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    Task<AppTokenResponse> CreateTokenByRefreshToken(string refreshToken, string accessToken);
}