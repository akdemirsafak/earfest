using earPass.Domain.Entities;

namespace earPass.Domain.Repositories;

public interface ITicketRepository : IGenericRepository<Ticket>
{
    Task<List<Ticket>> GetTicketsWithEvent();
    Task<Ticket> GetTicketByIdWithEvent(string eventId);
}
