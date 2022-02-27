using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitAPI.Models;

namespace UnitAPI.Repository
{
  public class UnitRepository : IUnitRepository
  {
    private readonly ApplicationDbContext _context;

    public UnitRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public ApplicationDbContext Context => _context;

    public async Task<IEnumerable<Unit>> GetAllAsync() => await _context.Units.ToListAsync();

    public async Task<Unit> GetAsync(int id, bool trackChanges = false)
    {
      if (trackChanges)
      {
        return await _context.Units.FirstOrDefaultAsync(e => e.Id == id);
      }

      return await _context.Units.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }
  }
}
