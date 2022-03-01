using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
  public class EmployeeController : Controller
  {
    private readonly IEmployeeService _service;
    private readonly ILogger _logger;

    public EmployeeController(IEmployeeService service, ILogger<EmployeeController> logger)
    {
      _service = service;
      _logger = logger;
    }

    // GET: Employee
    public async Task<IActionResult> Index()
    {
      await LogTokenAndClaims();
      return View(await _service.GetEmployeesAsync());
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

    // GET: Employee/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var employee = await _service.GetEmployeeByIdAsync((int)id);
      if (employee == null)
      {
        return NotFound();
      }

      return View(employee);
    }

    // GET: Employee/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Employee/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,CreatedBy,CreatedAt,IsActive")] Employee employee)
    {
      if (ModelState.IsValid)
      {
        await _service.CreateEmployeeAsync(employee);
        return RedirectToAction(nameof(Index));
      }
      return View(employee);
    }

    // GET: Employee/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var employee = await _service.GetEmployeeByIdAsync((int)id);
      if (employee == null)
      {
        return NotFound();
      }
      return View(employee);
    }

    // POST: Employee/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreatedBy,CreatedAt,IsActive")] Employee employee)
    {
      if (id != employee.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        await _service.UpdateEmployeeAsync(id, employee);
        return RedirectToAction(nameof(Index));
      }
      return View(employee);
    }

    // GET: Employee/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var employee = await _service.GetEmployeeByIdAsync((int)id);
      if (employee == null)
      {
        return NotFound();
      }

      return View(employee);
    }

    // POST: Employee/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      await _service.DeleteEmployeeAsync(id);
      return RedirectToAction(nameof(Index));
    }
  }
}
