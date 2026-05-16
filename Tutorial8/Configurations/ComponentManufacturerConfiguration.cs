using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutorial8.Entities;

namespace Tutorial8.Configurations;

public class ComponentManufacturerConfiguration : IEntityTypeConfiguration<ComponentManufacturer>
{
    public void Configure(EntityTypeBuilder<ComponentManufacturer> builder)
    {
        builder.HasKey(cm => cm.Id);
        builder.Property(cm => cm.Abbreviation).HasMaxLength(30).IsRequired();
        builder.Property(cm => cm.FullName).HasMaxLength(300).IsRequired();
        builder.HasData(
            new ComponentManufacturer { Id = 1, Abbreviation = "INTEL", FullName = "Integrated Electronics", FoundationDate = new DateOnly(1968, 1, 1) },
            new ComponentManufacturer { Id = 2, Abbreviation = "NV", FullName = "NVIDIA Corporation", FoundationDate = new DateOnly(1993, 1, 1) },
            new ComponentManufacturer { Id = 3, Abbreviation = "PM", FullName = "Patriot Memory", FoundationDate = new DateOnly(1985, 1, 1) },
            new ComponentManufacturer { Id = 4, Abbreviation = "AMD", FullName = "Advanced Micro Devices", FoundationDate = new DateOnly(1969, 5, 1) }
        );
    }
}