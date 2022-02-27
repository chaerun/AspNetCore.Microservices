using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitAPI.Models;
using UnitAPI.Repository;
using UnitAPI.ViewModels;

namespace UnitAPI.Services
{
  public class UnitService : IUnitService
  {
    private readonly IUnitRepository _repository;
    private readonly IMapper _mapper;

    public UnitService(IUnitRepository repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    public async Task<IEnumerable<UnitResponseDto>> GetUnitsAsync()
    {
      var units = await _repository.GetAllAsync();
      return _mapper.Map<IEnumerable<UnitResponseDto>>(units);
    }

    public async Task<UnitResponseDto> GetUnitByIdAsync(int id)
    {
      var unit = await _repository.GetAsync(id);
      return _mapper.Map<UnitResponseDto>(unit);
    }

    public async Task<UnitResponseDto> CreateUnitAsync(UnitRequestDto dto)
    {
      var unit = _mapper.Map<Unit>(dto);
      _repository.Context.Add(unit);
      await _repository.Context.SaveChangesAsync();

      return _mapper.Map<UnitResponseDto>(unit);
    }

    public async Task<bool> UpdateUnitAsync(int id, UnitRequestDto dto)
    {
      var unit = await _repository.GetAsync(id, true);
      if (unit == null)
      {
        return false;
      }

      _mapper.Map(dto, unit);
      await _repository.Context.SaveChangesAsync();
      return true;
    }

    public async Task<bool> DeleteUnitAsync(int id)
    {
      var unit = await _repository.GetAsync(id);
      if (unit == null)
      {
        return false;
      }

      _repository.Context.Remove(unit);
      await _repository.Context.SaveChangesAsync();
      return true;
    }
  }
}

