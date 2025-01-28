using earfest.API.Domain.Entities;
using earfest.API.Models.Category;
using earfest.API.Models.Moods;

namespace earfest.API.Models.Contents;

public class ContentDetailsResponse : BaseResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? AudioUrl { get; set; }
    public string? VideoUrl { get; set; }
    public string? Lyrics { get; set; }
    public virtual IList<MoodResponse> Moods { get; set; }
    public virtual IList<CategoryResponse> Categories { get; set; }
    public virtual IList<string> ArtistIds { get; set; } // Signifies the artist(s) of the content
}
