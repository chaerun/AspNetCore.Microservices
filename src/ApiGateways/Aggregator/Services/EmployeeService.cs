using Aggregator.Extensions;
using Aggregator.ViewModels;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aggregator.Services
{
  public class EmployeeService : IEmployeeService
  {
    private readonly HttpClient _client;

    public EmployeeService(HttpClient client)
    {
      _client = client;
    }

    public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
    {
      var response = await _client.GetAsync($"/api/employees/{id}");
      return await response.ReadContentAs<EmployeeDto>();
    }

    public async Task<PagedList<EmployeeDto>> GetEmployeesAsync()
    {
      var response = await _client.GetAsync("/api/employees");
      return await response.ReadContentAs<PagedList<EmployeeDto>>();
    }
  }
}

