using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutorial8.Entities;
namespace Tutorial8.Configurations;


public class PCConfiguration : IEntityTypeConfiguration<PC>
{
    public void Configure(EntityTypeBuilder<PC> builder)
    {
        builder.HasKey(pc => pc.Id);
        builder.Property(pc => pc.Name).HasMaxLength(50).IsRequired();
        builder.Property(pc => pc.Weight).IsRequired();
        builder.Property(pc => pc.Warranty).IsRequired();
        builder.Property(pc => pc.CreatedAt).IsRequired();
        builder.Property(pc => pc.Stock).IsRequired();
        builder.HasData(
            new PC { Id = 1, Name = "Gaming Monster x99", Weight = 30.0, Warranty = 36, CreatedAt = new DateTime(2026, 5, 16, 8, 0, 0), Stock = 69 },
            new PC { Id = 2, Name = "Pro Office Mini", Weight = 3.0, Warranty = 24, CreatedAt = new DateTime(2026, 5, 16, 8, 30, 0), Stock = 666 },
            new PC { Id = 3, Name = "Budget Bargain 13", Weight = 9.0, Warranty = 12, CreatedAt = new DateTime(2026, 5, 16, 9, 0, 0), Stock = 1 }
        );
    }
}