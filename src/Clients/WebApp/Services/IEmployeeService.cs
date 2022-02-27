using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Services
{
  public interface IEmployeeService
  {
    Task<IEnumerable<Employee>> GetEmployeesAsync();
    Task<Employee> GetEmployeeByIdAsync(int id);
    Task<Employee> CreateEmployeeAsync(Employee dto);
    Task<Employee> UpdateEmployeeAsync(int id, Employee dto);
    Task DeleteEmployeeAsync(int id);
  }
}
