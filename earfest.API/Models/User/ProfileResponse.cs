using earfest.API.Models.Playlists;

namespace earfest.API.Models.User;

public class ProfileResponse : BaseResponse
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? BirthDate { get; set; }
    public virtual IList<PlaylistResponse> Playlists { get; set; }
}
