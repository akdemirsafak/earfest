using earfest.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace earfest.API.Domain.Configurations;

public class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder.Property(prop => prop.Name)
            .IsRequired()
            .HasMaxLength(32);
        builder.Property(prop => prop.Description)
            .HasMaxLength(256);

    }
}
