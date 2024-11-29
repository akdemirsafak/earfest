namespace earfest.API.Models.Moods;

public class MoodResponse : BaseResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
}
