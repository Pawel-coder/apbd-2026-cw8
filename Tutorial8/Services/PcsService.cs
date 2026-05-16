using Microsoft.EntityFrameworkCore;
using Tutorial8.Data;
using Tutorial8.DTOs;
using Tutorial8.Entities;

namespace Tutorial8.Services;

public class PcsService : IPcsService
{
    private readonly AppDbContext _dbContext;

    public PcsService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<PcDto>> GetAllAsync()
    {
        return await _dbContext.PCs
            .Select(p => new PcDto
            {
                Id = p.Id,
                Name = p.Name,
                Weight = p.Weight,
                Warranty = p.Warranty,
                CreatedAt = p.CreatedAt,
                Stock = p.Stock
            }).ToListAsync();
    }
    public async Task<PcDetailsDto?> GetByIdWithComponentsAsync(int id)
    {
        var pc = await _dbContext.PCs
            .Include(p => p.PCComponents)
            .ThenInclude(pc => pc.Component)
            .ThenInclude(c => c.Manufacturer)
            .Include(p => p.PCComponents)
            .ThenInclude(pc => pc.Component)
            .ThenInclude(c => c.Type)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (pc == null) return null;
        return new PcDetailsDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock,
            Components = pc.PCComponents.Select(c => new PcComponentDetailsDto
            {
                Amount = c.Amount,
                Component = new ComponentItemDto
                {
                    Code = c.Component.Code,
                    Name = c.Component.Name,
                    Description = c.Component.Description,
                    Manufacturer = new ManufacturerDto
                    {
                        Id = c.Component.Manufacturer.Id,
                        Abbreviation = c.Component.Manufacturer.Abbreviation,
                        FullName = c.Component.Manufacturer.FullName,
                        FoundationDate = c.Component.Manufacturer.FoundationDate.ToString("yyyy-MM-dd")
                    },
                    Type = new TypeDto
                    {
                        Id = c.Component.Type.Id,
                        Abbreviation = c.Component.Type.Abbreviation,
                        Name = c.Component.Type.Name
                    }
                }
            }).ToList()
        };
    }
    public async Task<PcDto> CreateAsync(PcCreateUpdateDto dto)
    {
        var pc = new PC
        {
            Name = dto.Name,
            Weight = dto.Weight,
            Warranty = dto.Warranty,
            CreatedAt = dto.CreatedAt,
            Stock = dto.Stock
        };
        _dbContext.PCs.Add(pc);
        await _dbContext.SaveChangesAsync();
        return new PcDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }
    public async Task<bool> UpdateAsync(int id, PcCreateUpdateDto dto)
    {
        var pc = await _dbContext.PCs.FindAsync(id);
        if (pc == null) return false;
        pc.Name = dto.Name;
        pc.Weight = dto.Weight;
        pc.Warranty = dto.Warranty;
        pc.CreatedAt = dto.CreatedAt;
        pc.Stock = dto.Stock;
        await _dbContext.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var pc = await _dbContext.PCs.FindAsync(id);
        if (pc == null) return false;
        _dbContext.PCs.Remove(pc);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}