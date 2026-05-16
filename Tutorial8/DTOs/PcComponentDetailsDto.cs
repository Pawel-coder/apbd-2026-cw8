namespace Tutorial8.DTOs;

public class PcComponentDetailsDto
{
    public int Amount { get; set; }
    public ComponentItemDto Component { get; set; } = null!;
}