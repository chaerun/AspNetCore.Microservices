using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Services
{
  public interface IUnitService
  {
    Task<IEnumerable<Unit>> GetUnitsAsync();
    Task<Unit> GetUnitByIdAsync(int id);
    Task<Unit> CreateUnitAsync(Unit dto);
    Task<Unit> UpdateUnitAsync(int id, Unit dto);
    Task DeleteUnitAsync(int id);
  }
}
