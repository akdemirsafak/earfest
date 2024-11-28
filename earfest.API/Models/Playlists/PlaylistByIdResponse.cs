namespace earfest.API.Models.Playlists;

public class PlaylistByIdResponse: BaseResponse
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    //public virtual IList<ContentByIdResponse> Contents { get; set; }
}
