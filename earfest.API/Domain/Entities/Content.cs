namespace earfest.API.Domain.Entities;

public class Content : AbstractEntity, IAuditableEntity
{
    public Content()
    {
        Artists= new HashSet<AppUser>();
        Categories = new HashSet<Category>();
        Moods = new HashSet<Mood>();
    }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? AudioUrl { get; set; }
    public string? VideoUrl { get; set; }
    public string? Lyrics { get; set; }
    public ICollection<AppUser> Artists { get; set; }
    public ICollection<Category> Categories { get; set; }
    public ICollection<Mood> Moods { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
