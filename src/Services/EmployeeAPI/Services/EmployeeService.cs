using AutoMapper;
using EmployeeAPI.Models;
using EmployeeAPI.Repository;
using EmployeeAPI.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeAPI.Services
{
  public class EmployeeService : IEmployeeService
  {
    private readonly IEmployeeRepository _repository;
    private readonly IMapper _mapper;

    public EmployeeService(IEmployeeRepository repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    public async Task<IEnumerable<EmployeeResponseDto>> GetEmployeesAsync()
    {
      var units = await _repository.GetAllAsync();
      return _mapper.Map<IEnumerable<EmployeeResponseDto>>(units);
    }

    public async Task<EmployeeResponseDto> GetEmployeeByIdAsync(int id)
    {
      var unit = await _repository.GetAsync(id);
      return _mapper.Map<EmployeeResponseDto>(unit);
    }

    public async Task<EmployeeResponseDto> CreateEmployeeAsync(EmployeeRequestDto dto)
    {
      var unit = _mapper.Map<Employee>(dto);
      _repository.Context.Add(unit);
      await _repository.Context.SaveChangesAsync();

      return _mapper.Map<EmployeeResponseDto>(unit);
    }

    public async Task<bool> UpdateEmployeeAsync(int id, EmployeeRequestDto dto)
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

    public async Task<bool> DeleteEmployeeAsync(int id)
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

