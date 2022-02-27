using EmployeeAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeAPI.Repository
{
  public class EmployeeRepository : IEmployeeRepository
  {
    private readonly ApplicationDbContext _context;

    public EmployeeRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public ApplicationDbContext Context => _context;

    public async Task<IEnumerable<Employee>> GetAllAsync() => await _context.Employees.ToListAsync();

    public async Task<Employee> GetAsync(int id, bool trackChanges = false)
    {
      if (trackChanges)
      {
        return await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
      }

      return await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }
  }
}
