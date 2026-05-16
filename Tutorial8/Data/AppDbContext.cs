using Microsoft.EntityFrameworkCore;
using Tutorial8.Entities;

namespace Tutorial8.Data;

public class AppDbContext : DbContext
{
    protected AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<PC> PCs { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<PCComponent> PCComponents { get; set; }
    public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }
    public DbSet<ComponentType> ComponentTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PC>(e =>
        {
            e.HasKey(pc => pc.Id);
            e.Property(pc => pc.Name).HasMaxLength(50).IsRequired();
            e.Property(pc => pc.Weight).IsRequired();
            e.Property(pc => pc.Warranty).IsRequired();
            e.Property(pc => pc.CreatedAt).IsRequired();
            e.Property(pc => pc.Stock).IsRequired();
        });
        modelBuilder.Entity<PCComponent>(e =>
        {
            e.HasKey(pcc => new { pcc.PCId, pcc.ComponentCode });
                
            e.Property(pcc => pcc.ComponentCode).HasColumnType("char(10)");

            e.HasOne(pcc => pcc.PC)
                .WithMany(pc => pc.PCComponents)
                .HasForeignKey(pcc => pcc.PCId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(pcc => pcc.Component)
                .WithMany(c => c.PCComponents)
                .HasForeignKey(pcc => pcc.ComponentCode)
                .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<Component>(e =>
        {
            e.HasKey(c => c.Code);
            e.Property(c => c.Code).HasColumnType("char(10)").IsRequired();
            e.Property(c => c.Name).HasMaxLength(300).IsRequired();
            e.Property(c => c.Description).IsRequired();

            e.HasOne(c => c.Manufacturer)
                .WithMany(cm => cm.Components)
                .HasForeignKey(c => c.ComponentManufacturersId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(c => c.Type)
                .WithMany(ct => ct.Components)
                .HasForeignKey(c => c.ComponentTypesId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<ComponentType>(e =>
        {
            e.HasKey(ct => ct.Id);
            e.Property(ct => ct.Abbreviation).HasMaxLength(30).IsRequired();
            e.Property(ct => ct.Name).HasMaxLength(150).IsRequired();
        });
        modelBuilder.Entity<ComponentManufacturer>(e =>
        {
            e.HasKey(cm => cm.Id);
            e.Property(cm => cm.Abbreviation).HasMaxLength(30).IsRequired();
            e.Property(cm => cm.FullName).HasMaxLength(300).IsRequired();
        });
        modelBuilder.Entity<PC>().HasData(
            new PC { Id = 1, Name = "Gaming Monster x99", Weight = 30.0, Warranty = 36, CreatedAt = new DateTime(2026, 5, 16, 8, 0, 0), Stock = 69 },
            new PC { Id = 2, Name = "Pro Office Mini", Weight = 3.0, Warranty = 24, CreatedAt = new DateTime(2026, 5, 16, 8, 30, 0), Stock = 666 },
            new PC { Id = 3, Name = "Budget Bargain 13", Weight = 9.0, Warranty = 12, CreatedAt = new DateTime(2026, 5, 16, 9, 0, 0), Stock = 1 }
        );
        modelBuilder.Entity<PCComponent>().HasData(
            new PCComponent { PCId = 1, ComponentCode = "INTEL00001", Amount = 1 },
            new PCComponent { PCId = 1, ComponentCode = "NVIDIA0001", Amount = 1 },
            new PCComponent { PCId = 1, ComponentCode = "VIPER00001", Amount = 6 },
            new PCComponent { PCId = 2, ComponentCode = "AMD0000001", Amount = 1 },
            new PCComponent { PCId = 2, ComponentCode = "VIPER00002", Amount = 1 },
            new PCComponent { PCId = 3, ComponentCode = "VIPER00003", Amount = 3 }
        );
        modelBuilder.Entity<Component>().HasData(
            new Component { Code = "INTEL00001", Name = "Intel Core Ultra 99", Description = "16-core gaming processor", ComponentManufacturersId = 1, ComponentTypesId = 1 },
            new Component { Code = "NVIDIA0001", Name = "GeForce RTX 5070", Description = "Bleeding-edge gaming graphics card", ComponentManufacturersId = 2, ComponentTypesId = 2 },
            new Component { Code = "VIPER00001", Name = "Viper Venom DDR5 64GB", Description = "DDR5 RAM module 64GB", ComponentManufacturersId = 3, ComponentTypesId = 3 },
            new Component { Code = "AMD0000001", Name = "AMD Ryzen 9 9999X", Description = "mini-core pocket processor", ComponentManufacturersId = 4, ComponentTypesId = 1 },
            new Component { Code = "VIPER00002", Name = "Viper Venom DDR5 16GB", Description = "DDR5 RAM module 16GB", ComponentManufacturersId = 3, ComponentTypesId = 3 },
            new Component { Code = "VIPER00003", Name = "Viper Steel DDR4 8GB", Description = "DDR4 RAM module 8GB", ComponentManufacturersId = 3, ComponentTypesId = 3 }
        );
        modelBuilder.Entity<ComponentType>().HasData(
            new ComponentType { Id = 1, Abbreviation = "CPU", Name = "Processor" },
            new ComponentType { Id = 2, Abbreviation = "GPU", Name = "Graphics Card" },
            new ComponentType { Id = 3, Abbreviation = "RAM", Name = "Memory" }
        );
        modelBuilder.Entity<ComponentManufacturer>().HasData(
            new ComponentManufacturer { Id = 1, Abbreviation = "INTEL", FullName = "Integrated Electronics", FoundationDate = new DateOnly(1968, 1, 1) },
            new ComponentManufacturer { Id = 2, Abbreviation = "NV", FullName = "NVIDIA Corporation", FoundationDate = new DateOnly(1993, 1, 1) },
            new ComponentManufacturer { Id = 3, Abbreviation = "PM", FullName = "Patriot Memory", FoundationDate = new DateOnly(1985, 1, 1) },
            new ComponentManufacturer { Id = 4, Abbreviation = "AMD", FullName = "Advanced Micro Devices", FoundationDate = new DateOnly(1969, 5, 1) }
        );
        base.OnModelCreating(modelBuilder);
    }
}