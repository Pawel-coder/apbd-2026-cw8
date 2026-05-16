using Microsoft.AspNetCore.Mvc;
using Tutorial8.DTOs;
using Tutorial8.Services;

namespace DefaultNamespace;
[ApiController]
[Route("api/[controller]")]
public class PcsController : ControllerBase
{
    private readonly IPcsService _service;
    public PcsController(IPcsService service)
    {
        _service = service;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PcDto>>> GetAll()
    {
        var pcs = await _service.GetAllAsync();
        return Ok(pcs);
    }
    [HttpGet("{id}/components")]
    public async Task<ActionResult<PcDetailsDto>> GetComponents(int id)
    {
        var pcDetails = await _service.GetByIdWithComponentsAsync(id);
        if (pcDetails == null)
        {
            return NotFound();
        }
        return Ok(pcDetails);
    }
    [HttpPost]
    public async Task<ActionResult<PcDto>> Create([FromBody] PcCreateUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var createdPc = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetComponents), new { id = createdPc.Id }, createdPc);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] PcCreateUpdateDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var updated = await _service.UpdateAsync(id, dto);
        if (!updated)
        {
            return NotFound();
        }
        return Ok(dto);
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}