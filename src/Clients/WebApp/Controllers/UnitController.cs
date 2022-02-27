using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
  public class UnitController : Controller
  {
    private readonly IUnitService _service;

    public UnitController(IUnitService service)
    {
      _service = service;
    }

    // GET: Unit
    public async Task<IActionResult> Index()
    {
      return View(await _service.GetUnitsAsync());
    }

    // GET: Unit/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var unit = await _service.GetUnitByIdAsync((int)id);
      if (unit == null)
      {
        return NotFound();
      }

      return View(unit);
    }

    // GET: Unit/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Unit/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Code,CreatedBy,CreatedAt,IsActive")] Unit unit)
    {
      if (ModelState.IsValid)
      {
        await _service.CreateUnitAsync(unit);
        return RedirectToAction(nameof(Index));
      }
      return View(unit);
    }

    // GET: Unit/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var unit = await _service.GetUnitByIdAsync((int)id);
      if (unit == null)
      {
        return NotFound();
      }
      return View(unit);
    }

    // POST: Unit/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Code,CreatedBy,CreatedAt,IsActive")] Unit unit)
    {
      if (id != unit.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        await _service.UpdateUnitAsync(id, unit);
        return RedirectToAction(nameof(Index));
      }
      return View(unit);
    }

    // GET: Unit/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var unit = await _service.GetUnitByIdAsync((int)id);
      if (unit == null)
      {
        return NotFound();
      }

      return View(unit);
    }

    // POST: Unit/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      await _service.DeleteUnitAsync(id);
      return RedirectToAction(nameof(Index));
    }
  }
}
