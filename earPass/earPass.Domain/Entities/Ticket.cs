using earPass.Domain.Common;

namespace earPass.Domain.Entities;

public sealed class Ticket : AbstractEntity
{
    public Ticket()
    {
        Event = new Eventy();
    }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Eventy? Event { get; set; }
}
