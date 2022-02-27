using System;

namespace EmployeeAPI.Models
{
  public class Employee
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
  }
}
