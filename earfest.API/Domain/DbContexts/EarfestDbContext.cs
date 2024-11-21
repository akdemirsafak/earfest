using earfest.API.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace earfest.API.Domain.DbContexts;

public class EarfestDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public EarfestDbContext(DbContextOptions<EarfestDbContext> options) : base(options)
    {
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Content> Contents { get; set; }
    public DbSet<Mood> Moods { get; set; }
    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<Plan> Plans { get; set; }

}
