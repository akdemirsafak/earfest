using earPass.Domain.Common;
using earPass.Domain.Enums;

namespace earPass.Domain.Entities;

public sealed class Eventy : AbstractEntity, IAuditableEntity
{
    public Eventy()
    {
        Tickets = new HashSet<Ticket>();
        Performers = new HashSet<AppUser>();
    }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
    public string Image { get; set; }
    public EventTypeEnum Type { get; set; }
    public ICollection<Ticket> Tickets { get; set; }
    public ICollection<AppUser> Performers { get; set; }
    public DateTime CreatedAt { get ; set ; }
    public string? CreatedBy { get ; set ; }
    public DateTime? UpdatedAt { get ; set ; }
    public string? UpdatedBy { get ; set ; }
    public bool IsDeleted { get ; set ; }
    public DateTime? DeletedAt { get ; set ; }
    public string? DeletedBy { get ; set ; }
}
