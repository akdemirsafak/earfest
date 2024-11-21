using earfest.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace earfest.API.Domain.Configurations
{
    public class ContentConfiguration : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {
            builder.Property(prop => prop.Name)
                .IsRequired()
                .HasMaxLength(32);
            builder.Property(prop => prop.Description)
                .HasMaxLength(256);
            builder.Property(prop => prop.Lyrics)
                .HasMaxLength(4096);
        }
    }
}
