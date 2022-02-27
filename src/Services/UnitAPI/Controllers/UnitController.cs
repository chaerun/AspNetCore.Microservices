using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using UnitAPI.Services;
using UnitAPI.ViewModels;

namespace UnitAPI.Controllers
{
  [Route("api/units")]
  [ApiController]
  public class UnitController : ControllerBase
  {
    private readonly IUnitService _service;

    public UnitController(IUnitService service)
    {
      _service = service;
    }

    [HttpGet]
    public async Task<ActionResult> GetUnits()
    {
      var dto = await _service.GetUnitsAsync();
      var model = new PagedList<UnitResponseDto>(true, "success", dto.LongCount(), dto);
      return Ok(model);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UnitRequestDto>> GetUnit(int id)
    {
      var unit = await _service.GetUnitByIdAsync(id);
      return Ok(unit);
    }

    [HttpPost]
    public async Task<ActionResult<UnitResponseDto>> PostUnit(UnitRequestDto dto)
    {
      var responseDto = await _service.CreateUnitAsync(dto);
      return CreatedAtAction("GetUnit", new { id = responseDto.Id }, responseDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUnit(int id, UnitRequestDto unitToUpdate)
    {
      var result = await _service.UpdateUnitAsync(id, unitToUpdate);
      return result ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUnit(int id)
    {
      var result = await _service.DeleteUnitAsync(id);
      return result ? NoContent() : NotFound();
    }
  }
}
