using System;

namespace Aggregator.ViewModels
{
  public class EmployeeResponseModel
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public UnitDto Unit { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
  }
}
