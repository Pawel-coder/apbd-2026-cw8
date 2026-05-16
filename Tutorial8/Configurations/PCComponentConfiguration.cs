using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tutorial8.Entities;

namespace Tutorial8.Configurations;

public class PCComponentConfiguration : IEntityTypeConfiguration<PCComponent>
{
    public void Configure(EntityTypeBuilder<PCComponent> builder)
    {
        builder.HasKey(pcc => new { pcc.PCId, pcc.ComponentCode });
                
        builder.Property(pcc => pcc.ComponentCode).HasColumnType("char(10)");

        builder.HasOne(pcc => pcc.PC)
            .WithMany(pc => pc.PCComponents)
            .HasForeignKey(pcc => pcc.PCId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pcc => pcc.Component)
            .WithMany(c => c.PCComponents)
            .HasForeignKey(pcc => pcc.ComponentCode)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasData(
            new PCComponent { PCId = 1, ComponentCode = "INTEL00001", Amount = 1 },
            new PCComponent { PCId = 1, ComponentCode = "NVIDIA0001", Amount = 1 },
            new PCComponent { PCId = 1, ComponentCode = "VIPER00001", Amount = 6 },
            new PCComponent { PCId = 2, ComponentCode = "AMD0000001", Amount = 1 },
            new PCComponent { PCId = 2, ComponentCode = "VIPER00002", Amount = 1 },
            new PCComponent { PCId = 3, ComponentCode = "VIPER00003", Amount = 3 }
        );
    }
}