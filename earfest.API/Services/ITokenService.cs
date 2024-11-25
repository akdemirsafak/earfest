using earfest.API.Models.Auth;

namespace earfest.API.Services;

public interface ITokenService
{
    string GetEmailFromToken(string token);
    string GetToken(string token);

}
