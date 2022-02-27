using System;

namespace EmployeeAPI.ViewModels
{
  public class EmployeeResponseDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
  }
}
