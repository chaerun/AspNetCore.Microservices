using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Services
{
  public class EmployeeService : IEmployeeService
  {
    private readonly IHttpClientFactory _clientFactory;

    public EmployeeService(IHttpClientFactory clientFactory)
    {
      _clientFactory = clientFactory;
    }

    public async Task<Employee> CreateEmployeeAsync(Employee dto)
    {
      var httpClient = _clientFactory.CreateClient("web.client");

      var request = new HttpRequestMessage(HttpMethod.Post, "/employees");
      var json = JsonConvert.SerializeObject(dto);
      request.Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

      var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

      var content = await response.Content.ReadAsStringAsync();
      return JsonConvert.DeserializeObject<Employee>(content);
    }

    public async Task DeleteEmployeeAsync(int id)
    {
      var httpClient = _clientFactory.CreateClient("web.client");

      var request = new HttpRequestMessage(HttpMethod.Delete, $"/employees/{id}");
      await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
    }

    public async Task<Employee> GetEmployeeByIdAsync(int id)
    {
      var httpClient = _clientFactory.CreateClient("web.client");

      var request = new HttpRequestMessage(HttpMethod.Get, $"/units/{id}");
      var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

      var content = await response.Content.ReadAsStringAsync();
      return JsonConvert.DeserializeObject<Employee>(content);
    }

    public async Task<IEnumerable<Employee>> GetEmployeesAsync()
    {
      var httpClient = _clientFactory.CreateClient("web.client");

      var request = new HttpRequestMessage(HttpMethod.Get, "/employees");
      var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

      var content = await response.Content.ReadAsStringAsync();
      var employees = JsonConvert.DeserializeObject<PagedList<Employee>>(content);

      return employees.Data;
    }

    public async Task<Employee> UpdateEmployeeAsync(int id, Employee dto)
    {
      var httpClient = _clientFactory.CreateClient("web.client");

      var request = new HttpRequestMessage(HttpMethod.Put, $"/units/{id}");
      var json = JsonConvert.SerializeObject(dto);
      request.Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

      var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

      var content = await response.Content.ReadAsStringAsync();
      return JsonConvert.DeserializeObject<Employee>(content);
    }
  }
}

