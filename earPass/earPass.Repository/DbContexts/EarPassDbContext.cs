using earPass.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace earPass.Repository.DbContexts;

public class EarPassDbContext : IdentityDbContext<AppUser,AppRole,string>
{
    public EarPassDbContext(DbContextOptions<EarPassDbContext> options) : base(options)
    {
    }

    public DbSet<Eventy> Events { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<UserTicket> UserTickets { get; set; }

}
