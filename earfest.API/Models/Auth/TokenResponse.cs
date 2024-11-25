namespace earfest.API.Models.Auth;

public class TokenResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public DateTime Expires { get; set; }
    public DateTime RefreshTokenExpires { get; set; }
}
