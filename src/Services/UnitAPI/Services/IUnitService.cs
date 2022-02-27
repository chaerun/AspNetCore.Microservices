using System.Collections.Generic;
using System.Threading.Tasks;
using UnitAPI.ViewModels;

namespace UnitAPI.Services
{
  public interface IUnitService
  {
    Task<IEnumerable<UnitResponseDto>> GetUnitsAsync();
    Task<UnitResponseDto> GetUnitByIdAsync(int id);
    Task<UnitResponseDto> CreateUnitAsync(UnitRequestDto dto);
    Task<bool> UpdateUnitAsync(int id, UnitRequestDto dto);
    Task<bool> DeleteUnitAsync(int id);
  }
}
