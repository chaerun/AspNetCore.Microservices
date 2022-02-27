using Aggregator.Extensions;
using Aggregator.ViewModels;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aggregator.Services
{
  public class UnitService : IUnitService
  {
    private readonly HttpClient _client;

    public UnitService(HttpClient client)
    {
      _client = client;
    }

    public async Task<UnitDto> GetUnitByIdAsync(int id)
    {
      var response = await _client.GetAsync($"/api/units/{id}");
      return await response.ReadContentAs<UnitDto>();
    }

    public async Task<PagedList<UnitDto>> GetUnitsAsync()
    {
      var response = await _client.GetAsync("/api/units");
      return await response.ReadContentAs<PagedList<UnitDto>>();
    }
  }
}
