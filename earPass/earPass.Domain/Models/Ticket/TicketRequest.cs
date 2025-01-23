namespace earPass.Domain.Models.Ticket;

public record TicketRequest(string Name,string Description,decimal Price,int Quantity,string EventId);