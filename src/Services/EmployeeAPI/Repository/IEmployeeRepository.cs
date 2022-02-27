using EmployeeAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeAPI.Repository
{
  public interface IEmployeeRepository
  {
    ApplicationDbContext Context { get; }

    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee> GetAsync(int id, bool trackChanges = false);
  }
}
