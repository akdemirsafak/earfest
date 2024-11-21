using earfest.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace earfest.API.Domain.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(prop => prop.Name)
            .IsRequired()
            .HasMaxLength(32);
        builder.Property(prop => prop.Description)
            .HasMaxLength(256);
    }
}
