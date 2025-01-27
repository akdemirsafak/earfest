namespace Earfest.Auth.Models.User;

public class ProfileResponse : BaseResponse
{
    public string Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? BirthDate { get; set; }
}
