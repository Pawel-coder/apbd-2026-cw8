namespace Tutorial8.Entities;

public class PC
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public double Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }

    public ICollection<PCComponent> PCComponents { get; set; } = [];
}