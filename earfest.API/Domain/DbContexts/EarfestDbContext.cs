using earfest.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace earfest.API.Domain.DbContexts;

public class EarfestDbContext : DbContext
{
    public EarfestDbContext(DbContextOptions<EarfestDbContext> options) : base(options)
    {
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Content> Contents { get; set; }
    public DbSet<Mood> Moods { get; set; }
    public DbSet<Playlist> Playlists { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Category>().HasQueryFilter(p => !p.IsDeleted);
        builder.Entity<Content>().HasQueryFilter(p => !p.IsDeleted);
        builder.Entity<Playlist>().HasQueryFilter(p => !p.IsDeleted);
        builder.Entity<Mood>().HasQueryFilter(p => !p.IsDeleted);
        base.OnModelCreating(builder);
    }
}
