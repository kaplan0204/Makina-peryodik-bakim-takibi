using MakinaPeryodikBakimTakibi.Data;
using MakinaPeryodikBakimTakibi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MakinaPeryodikBakimTakibi.Controllers;

public class MaintenanceRecordController : Controller
{
    private readonly ApplicationDbContext _context;

    public MaintenanceRecordController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var model = _context.MaintenanceRecords
            .Include(m => m.Machine)
            .Include(m => m.ResponsibleEmployee)
            .Include(m => m.Department)
            .OrderByDescending(m => m.MaintenanceDate)
            .ToList();
        return View(model);
    }

    public IActionResult Create()
    {
        ViewBag.Machines = _context.Machines.OrderBy(m => m.Name).ToList();
        ViewBag.Employees = _context.Employees.OrderBy(e => e.LastName).ToList();
        return View(new MaintenanceRecord { MaintenanceDate = DateTime.Today, CreatedAt = DateTime.Today });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(MaintenanceRecord model)
    {
        if (ModelState.IsValid)
        {
            model.CreatedAt = DateTime.Now;
            model.UpdatedAt = DateTime.Now;
            _context.MaintenanceRecords.Add(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Machines = _context.Machines.OrderBy(m => m.Name).ToList();
        ViewBag.Employees = _context.Employees.OrderBy(e => e.LastName).ToList();
        return View(model);
    }
}
