using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Services
{
  public class EmployeeService : IEmployeeService
  {
    public Task<Employee> CreateEmployeeAsync(Employee dto)
    {
      throw new System.NotImplementedException();
    }

    public Task DeleteEmployeeAsync(int id)
    {
      throw new System.NotImplementedException();
    }

    public Task<Employee> GetEmployeeByIdAsync(int id)
    {
      throw new System.NotImplementedException();
    }

    public Task<IEnumerable<Employee>> GetEmployeesAsync()
    {
      throw new System.NotImplementedException();
    }

    public Task<Employee> UpdateEmployeeAsync(int id, Employee dto)
    {
      throw new System.NotImplementedException();
    }
  }
}

