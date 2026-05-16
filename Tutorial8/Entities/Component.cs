namespace Tutorial8.Entities;

public class Component
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; } 
    public int ComponentManufacturersId { get; set; }
    public ComponentManufacturer Manufacturer { get; set; } = null!;
    public int ComponentTypesId { get; set; } 
    
    public ComponentType Type { get; set; } = null!;
    public ICollection<PCComponent> PCComponents { get; set; } = [];
}