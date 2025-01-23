using earPass.Domain.Entities;
using earPass.Domain.Repositories;
using earPass.Repository.DbContexts;

namespace earPass.Repository.Repositories;

public sealed class TicketRepository : GenericRepository<Ticket>, ITicketRepository
{
    public TicketRepository(EarPassDbContext dbContext) : base(dbContext)
    {
    }
}
