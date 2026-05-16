using Tutorial8.DTOs;

namespace Tutorial8.Services;

public interface IPcsService
{
    Task<IEnumerable<PcDto>> GetAllAsync();
    Task<PcDetailsDto?> GetByIdWithComponentsAsync(int id);
    Task<PcDto> CreateAsync(PcCreateUpdateDto dto);
    Task<bool> UpdateAsync(int id, PcCreateUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}