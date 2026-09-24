using MakinaPeryodikBakimTakibi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MakinaPeryodikBakimTakibi.Controllers;

public class PeriodicMaintenanceController : Controller
{
    private readonly Data.ApplicationDbContext _context;

    public PeriodicMaintenanceController(Data.ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var model = _context.PeriodicMaintenances
            .Include(nameof(Machine))
            .ToList();
        return View(model);
    }
}
