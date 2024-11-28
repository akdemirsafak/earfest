namespace earfest.API.Models.Contents;

public class ContentByIdResponse : BaseResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? AudioUrl { get; set; }
    public string? VideoUrl { get; set; }
    public string? Lyrics { get; set; }
    
}
