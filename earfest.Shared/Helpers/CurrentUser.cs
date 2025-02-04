using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace earfest.Shared.Helpers;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

    public string GetEmail => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);

    public string GetUserName => _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
}
