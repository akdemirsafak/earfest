namespace earfest.API.Models.Playlists;

public class PlaylistResponse : BaseResponse
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
}
