using System.ComponentModel.DataAnnotations;

namespace EmployeeAPI.ViewModels
{
  public class EmployeeRequestDto
  {
    [Required]
    public string Name { get; set; }
  }
}
