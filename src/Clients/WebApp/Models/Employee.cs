using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
  public class Employee
  {
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public int UnitId { get; set; }

    public Unit Unit { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
  }
}
