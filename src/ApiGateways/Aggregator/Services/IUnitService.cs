using Aggregator.ViewModels;
using System.Threading.Tasks;

namespace Aggregator.Services
{
  public interface IUnitService
  {
    Task<PagedList<UnitDto>> GetUnitsAsync();
    Task<UnitDto> GetUnitByIdAsync(int id);
  }
}
