using System.ComponentModel.DataAnnotations;

namespace UnitAPI.ViewModels
{
  public class UnitRequestDto
  {
    [Required]
    public string Name { get; set; }
    [Required]
    public string Code { get; set; }
  }
}
