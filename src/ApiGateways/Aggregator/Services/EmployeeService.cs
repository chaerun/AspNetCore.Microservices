using Aggregator.Extensions;
using Aggregator.ViewModels;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aggregator.Services
{
  public class EmployeeService : IEmployeeService
  {
    private readonly IHttpClientFactory _clientFactory;

    public EmployeeService(IHttpClientFactory clientFactory)
    {
      _clientFactory = clientFactory;
    }

    public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
    {
      var httpClient = _clientFactory.CreateClient("employee.client");

      var request = new HttpRequestMessage(HttpMethod.Get, $"/api/employees/{id}");
      var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

      return await response.ReadContentAs<EmployeeDto>();
    }

    public async Task<PagedList<EmployeeDto>> GetEmployeesAsync()
    {
      var httpClient = _clientFactory.CreateClient("employee.client");

      var request = new HttpRequestMessage(HttpMethod.Get, "/api/employees");
      var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

      return await response.ReadContentAs<PagedList<EmployeeDto>>();
    }
  }
}

