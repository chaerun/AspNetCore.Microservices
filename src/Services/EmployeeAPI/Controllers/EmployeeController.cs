using EmployeeAPI.Services;
using EmployeeAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Controllers
{
  [Route("api/employees")]
  [ApiController]
  [Authorize("ApiScopePolicy")]
  public class EmployeeController : ControllerBase
  {
    private readonly IEmployeeService _service;

    public EmployeeController(IEmployeeService service)
    {
      _service = service;
    }

    [HttpGet]
    public async Task<ActionResult> GetEmployees()
    {
      var dto = await _service.GetEmployeesAsync();
      var model = new PagedList<EmployeeResponseDto>(true, "success", dto.LongCount(), dto);
      return Ok(model);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeRequestDto>> GetEmployee(int id)
    {
      var employee = await _service.GetEmployeeByIdAsync(id);
      return Ok(employee);
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeResponseDto>> PostEmployee(EmployeeRequestDto dto)
    {
      var responseDto = await _service.CreateEmployeeAsync(dto);
      return CreatedAtAction("GetEmployee", new { id = responseDto.Id }, responseDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutEmployee(int id, EmployeeRequestDto unitToUpdate)
    {
      var result = await _service.UpdateEmployeeAsync(id, unitToUpdate);
      return result ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
      var result = await _service.DeleteEmployeeAsync(id);
      return result ? NoContent() : NotFound();
    }
  }
}
