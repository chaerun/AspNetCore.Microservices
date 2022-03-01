using Aggregator.Extensions;
using Aggregator.ViewModels;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aggregator.Services
{
  public class UnitService : IUnitService
  {
    private readonly IHttpClientFactory _clientFactory;

    public UnitService(IHttpClientFactory clientFactory)
    {
      _clientFactory = clientFactory;
    }

    public async Task<UnitDto> GetUnitByIdAsync(int id)
    {
      var httpClient = _clientFactory.CreateClient("unit.client");

      var request = new HttpRequestMessage(HttpMethod.Get, $"/api/units/{id}");
      var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
      response.EnsureSuccessStatusCode();

      return await response.ReadContentAs<UnitDto>();
    }

    public async Task<PagedList<UnitDto>> GetUnitsAsync()
    {
      var httpClient = _clientFactory.CreateClient("unit.client");

      var request = new HttpRequestMessage(HttpMethod.Get, "/api/units");
      var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
      response.EnsureSuccessStatusCode();

      return await response.ReadContentAs<PagedList<UnitDto>>();
    }
  }
}
