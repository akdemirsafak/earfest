using earPass.Domain.Entities;
using earPass.Domain.Repositories;
using earPass.Repository.DbContexts;

namespace earPass.Repository.Repositories;

public sealed class EventyRepository : GenericRepository<Eventy>, IEventyRepository
{
    public EventyRepository(EarPassDbContext dbContext) : base(dbContext)
    {
    }
}