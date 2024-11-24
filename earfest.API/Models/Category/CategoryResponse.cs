namespace earfest.API.Models.Category;

public class CategoryResponse : BaseResponse
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    //public virtual IList<ContentResponse> Contents { get; set; }

}
