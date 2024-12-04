
using System.ComponentModel;

namespace earfest.API.Domain.Entities;

public class Category : AbstractEntity, IAuditableEntity
{
    public Category()
    {
        Contents= new HashSet<Content>();
    }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public ICollection<Content> Contents { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}
