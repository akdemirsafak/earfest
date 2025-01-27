using earPass.Domain.Common;
using earPass.Domain.Enums;

namespace earPass.Domain.Entities;

public sealed class Eventy : AbstractEntity
{
    public Eventy()
    {
        Tickets = new HashSet<Ticket>();
        PerformerIds = new HashSet<string>();
    }
    public string Name { get; set; }
    public string? Description { get; set; }
    public DateTime? Date { get; set; }
    public string Location { get; set; }
    public string? Image { get; set; }
    public EventTypeEnum Type { get; set; }
    public ICollection<Ticket>? Tickets { get; set; }
    public ICollection<string>? PerformerIds { get; set; }
}
