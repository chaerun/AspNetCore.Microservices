using System;

namespace WebApp.Models
{
  public class Employee
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public Unit Unit { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
  }
}
