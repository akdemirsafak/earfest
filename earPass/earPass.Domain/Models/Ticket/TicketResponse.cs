namespace earPass.Domain.Models.Ticket;

public sealed class TicketResponse : BaseResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string EventId { get; set; }
    public string EventName { get; set; }
}
