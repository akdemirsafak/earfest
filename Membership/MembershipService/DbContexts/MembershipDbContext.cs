using MembershipService.Entities;
using Microsoft.EntityFrameworkCore;

namespace MembershipService.DbContexts;

public class MembershipDbContext : DbContext
{
    public MembershipDbContext(DbContextOptions<MembershipDbContext> options) : base(options)
    {
    }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
}
