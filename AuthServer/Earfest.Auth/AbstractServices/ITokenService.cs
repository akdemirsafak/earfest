using Earfest.Auth.Entities;
using Earfest.Auth.Models;
using System.Security.Claims;

namespace Earfest.Auth.AbstractServices;

public interface ITokenService
{
    Task<AppTokenResponse> CreateTokenAsync(AppUser appUser);
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    Task<AppTokenResponse> CreateTokenByRefreshToken(string refreshToken, string accessToken);
}