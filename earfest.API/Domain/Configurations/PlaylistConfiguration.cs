using earfest.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace earfest.API.Domain.Configurations;

public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
{
    public void Configure(EntityTypeBuilder<Playlist> builder)
    {
        builder.Property(prop => prop.Name)
            .IsRequired()
            .HasMaxLength(32);
        builder.Property(prop => prop.Description)
            .HasMaxLength(256);
    }
}
