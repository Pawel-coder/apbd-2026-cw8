using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutorial8.Entities;

namespace Tutorial8.Configurations;

public class ComponentConfiguration : IEntityTypeConfiguration<Component>
{
    public void Configure(EntityTypeBuilder<Component> builder)
    {
        builder.HasKey(c => c.Code);
        builder.Property(c => c.Code).HasColumnType("char(10)").IsRequired();
        builder.Property(c => c.Name).HasMaxLength(300).IsRequired();
        builder.Property(c => c.Description).IsRequired();

        builder.HasOne(c => c.Manufacturer)
            .WithMany(cm => cm.Components)
            .HasForeignKey(c => c.ComponentManufacturersId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Type)
            .WithMany(ct => ct.Components)
            .HasForeignKey(c => c.ComponentTypesId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasData(
            new Component { Code = "INTEL00001", Name = "Intel Core Ultra 99", Description = "16-core gaming processor", ComponentManufacturersId = 1, ComponentTypesId = 1 },
            new Component { Code = "NVIDIA0001", Name = "GeForce RTX 5070", Description = "Bleeding-edge gaming graphics card", ComponentManufacturersId = 2, ComponentTypesId = 2 },
            new Component { Code = "VIPER00001", Name = "Viper Venom DDR5 64GB", Description = "DDR5 RAM module 64GB", ComponentManufacturersId = 3, ComponentTypesId = 3 },
            new Component { Code = "AMD0000001", Name = "AMD Ryzen 9 9999X", Description = "mini-core pocket processor", ComponentManufacturersId = 4, ComponentTypesId = 1 },
            new Component { Code = "VIPER00002", Name = "Viper Venom DDR5 16GB", Description = "DDR5 RAM module 16GB", ComponentManufacturersId = 3, ComponentTypesId = 3 },
            new Component { Code = "VIPER00003", Name = "Viper Steel DDR4 8GB", Description = "DDR4 RAM module 8GB", ComponentManufacturersId = 3, ComponentTypesId = 3 }
        );
    }
}