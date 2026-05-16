namespace Tutorial8.Entities;

public class ComponentManufacturer
{
    public int Id { get; set; }
    public required string Abbreviation { get; set; }
    public required string FullName { get; set; }
    public DateOnly FoundationDate { get; set; }

    public ICollection<Component> Components { get; set; } = [];
}