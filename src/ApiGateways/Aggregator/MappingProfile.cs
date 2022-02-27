using Aggregator.ViewModels;
using AutoMapper;

namespace Aggregator
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<EmployeeDto, EmployeeResponseModel>();
    }
  }
}