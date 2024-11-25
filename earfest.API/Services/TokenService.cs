namespace earfest.API.Services;

public class TokenService : ITokenService
{
    public string GetEmailFromToken(string token)
    {
        return token.Split('.')[0];
    }
    public string GetToken(string token)
    {
        return token.Split('.')[1];
    }
}
