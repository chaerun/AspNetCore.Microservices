using EmployeeAPI.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeAPI.Services
{
  public interface IEmployeeService
  {
    Task<IEnumerable<EmployeeResponseDto>> GetEmployeesAsync();
    Task<EmployeeResponseDto> GetEmployeeByIdAsync(int id);
    Task<EmployeeResponseDto> CreateEmployeeAsync(EmployeeRequestDto dto);
    Task<bool> UpdateEmployeeAsync(int id, EmployeeRequestDto dto);
    Task<bool> DeleteEmployeeAsync(int id);
  }
}
