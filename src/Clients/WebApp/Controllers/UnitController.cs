using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
  [Authorize]
  public class UnitController : Controller
  {
    private readonly IUnitService _service;
    private readonly ILogger _logger;

    public UnitController(IUnitService service, ILogger<UnitController> logger)
    {
      _service = service;
      _logger = logger;
    }

    // GET: Unit
    public async Task<IActionResult> Index()
    {
      await LogTokenAndClaims();
      return View(await _service.GetUnitsAsync());
    }

    public async Task LogTokenAndClaims()
    {
      var identityToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);
      _logger.LogInformation($"Identity token: {identityToken}", identityToken);

      foreach (var claim in User.Claims)
      {
        _logger.LogInformation($"Claim type: {claim.Type} - Claim value: {claim.Value}");
      }
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
