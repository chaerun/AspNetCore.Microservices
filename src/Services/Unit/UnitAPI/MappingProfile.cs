using AutoMapper;
using UnitAPI.Models;
using UnitAPI.ViewModels;

namespace UnitAPI
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Unit, UnitResponseDto>();
      CreateMap<UnitRequestDto, Unit>();
      CreateMap<UnitRequestDto, Unit>().ReverseMap();
    }
  }
}