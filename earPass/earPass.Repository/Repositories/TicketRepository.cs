using earPass.Domain.Entities;
using earPass.Domain.Repositories;
using earPass.Repository.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace earPass.Repository.Repositories;

public sealed class TicketRepository : GenericRepository<Ticket>, ITicketRepository
{
    public TicketRepository(EarPassDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Ticket> GetTicketByIdWithEvent(string eventId)
    {
        var ticket = await _dbContext.Tickets.Include(x => x.Event).FirstOrDefaultAsync(x => x.Event.Id == eventId);
        return ticket;
    }

    public async Task<List<Ticket>> GetTicketsWithEvent()
    {
        var tickets= await _dbContext.Tickets.Include(x => x.Event).ToListAsync();
        return tickets;
    }
}
