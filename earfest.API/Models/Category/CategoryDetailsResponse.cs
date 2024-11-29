using earfest.API.Models.Contents;

namespace earfest.API.Models.Category;

public class CategoryDetailsResponse : BaseResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public IList<ContentResponse> Contents { get; set; }
}