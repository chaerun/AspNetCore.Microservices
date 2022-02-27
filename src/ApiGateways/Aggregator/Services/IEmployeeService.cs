using Aggregator.ViewModels;
using System.Threading.Tasks;

namespace Aggregator.Services
{
  public interface IEmployeeService
  {
    Task<PagedList<EmployeeDto>> GetEmployeesAsync();
    Task<EmployeeDto> GetEmployeeByIdAsync(int id);
  }
}
