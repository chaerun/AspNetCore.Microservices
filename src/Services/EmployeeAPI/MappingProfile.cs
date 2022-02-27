using AutoMapper;
using EmployeeAPI.Models;
using EmployeeAPI.ViewModels;

namespace EmployeeAPI
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Employee, EmployeeResponseDto>();
      CreateMap<EmployeeRequestDto, Employee>();
      CreateMap<EmployeeRequestDto, Employee>().ReverseMap();
    }
  }
}