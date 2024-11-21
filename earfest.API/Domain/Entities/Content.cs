
namespace earfest.API.Domain.Entities;

public class Content : AbstractEntity, IAuditableEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? AudioUrl { get; set; }
    public string? VideoUrl { get; set; }
    public string? Lyrics { get; set; }
    public List<AppUser> Artists { get; set; }
    public List<Category> Categories { get; set; }
    public List<Mood> Moods { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
