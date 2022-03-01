using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Services
{
  public class UnitService : IUnitService
  {
    private readonly IHttpClientFactory _clientFactory;

    public UnitService(IHttpClientFactory clientFactory)
    {
      _clientFactory = clientFactory;
    }

    public async Task<Unit> CreateUnitAsync(Unit dto)
    {
      var httpClient = _clientFactory.CreateClient("web.client");

      var request = new HttpRequestMessage(HttpMethod.Post, "/units");
      var json = JsonConvert.SerializeObject(dto);
      request.Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

      var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

      var content = await response.Content.ReadAsStringAsync();
      return JsonConvert.DeserializeObject<Unit>(content);
    }

    public async Task DeleteUnitAsync(int id)
    {
      var httpClient = _clientFactory.CreateClient("web.client");

      var request = new HttpRequestMessage(HttpMethod.Delete, $"/units/{id}");
      await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
    }

    public async Task<Unit> GetUnitByIdAsync(int id)
    {
      var httpClient = _clientFactory.CreateClient("web.client");

      var request = new HttpRequestMessage(HttpMethod.Get, $"/units/{id}");
      var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

      var content = await response.Content.ReadAsStringAsync();
      return JsonConvert.DeserializeObject<Unit>(content);
    }

    public async Task<IEnumerable<Unit>> GetUnitsAsync()
    {
      var httpClient = _clientFactory.CreateClient("web.client");

      var request = new HttpRequestMessage(HttpMethod.Get, "/units");
      var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

      var content = await response.Content.ReadAsStringAsync();
      var units = JsonConvert.DeserializeObject<PagedList<Unit>>(content);

      return units.Data;
    }

    public async Task<Unit> UpdateUnitAsync(int id, Unit dto)
    {
      var httpClient = _clientFactory.CreateClient("web.client");

      var request = new HttpRequestMessage(HttpMethod.Put, $"/units/{id}");
      var json = JsonConvert.SerializeObject(dto);
      request.Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

      var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

      var content = await response.Content.ReadAsStringAsync();
      return JsonConvert.DeserializeObject<Unit>(content);
    }
  }
}

