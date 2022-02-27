using Aggregator.Services;
using Aggregator.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Aggregator.Controllers
{
  [Route("api/employee-details")]
  [ApiController]
  public class EmployeeDetailController : ControllerBase
  {
    private readonly IUnitService _unitService;
    private readonly IEmployeeService _employeeService;
    private readonly IMapper _mapper;

    public EmployeeDetailController(IUnitService unitService, IEmployeeService employeeService, IMapper mapper)
    {
      _unitService = unitService;
      _employeeService = employeeService;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult> GetEmployees()
    {
      var pagedEmployees = await _employeeService.GetEmployeesAsync();
      var pagedUnits = await _unitService.GetUnitsAsync();

      var responseDtos = pagedEmployees.Data.GroupJoin(
        pagedUnits.Data,
        employee => employee.UnitId,
        unit => unit.Id,
        (employee, unitList) =>
        {
          var response = _mapper.Map<EmployeeResponseModel>(employee);
          response.Unit = unitList.FirstOrDefault(u => u.Id == employee.Id);
          return response;
        });

      var model = new PagedList<EmployeeResponseModel>(true, "success", responseDtos.LongCount(), responseDtos);
      return Ok(model);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeResponseModel>> GetEmployee(int id)
    {
      var employee = await _employeeService.GetEmployeeByIdAsync(id);
      if (employee == null)
      {
        return NotFound();
      }

      var response = _mapper.Map<EmployeeResponseModel>(employee);

      var unit = await _unitService.GetUnitByIdAsync(employee.UnitId);
      response.Unit = unit;

      return Ok(response);
    }
  }
}
