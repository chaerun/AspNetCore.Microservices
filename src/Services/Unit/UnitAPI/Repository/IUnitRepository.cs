using System.Collections.Generic;
using System.Threading.Tasks;
using UnitAPI.Models;

namespace UnitAPI.Repository
{
  public interface IUnitRepository
  {
    ApplicationDbContext Context { get; }

    Task<IEnumerable<Unit>> GetAllAsync();
    Task<Unit> GetAsync(int id, bool trackChanges = false);
  }
}
