using MakinaPeryodikBakimTakibi.Data;
using MakinaPeryodikBakimTakibi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MakinaPeryodikBakimTakibi.Controllers;

public class MachineController : Controller
{
    private readonly ApplicationDbContext _context;

    public MachineController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var model = _context.Machines
            .Include(m => m.Department)
            .OrderBy(m => m.Name)
            .ToList();
        return View(model);
    }

    public IActionResult Create()
    {
        ViewBag.Departments = _context.Departments.OrderBy(d => d.Name).ToList();
        return View(new Machine());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Machine model)
    {
        if (ModelState.IsValid)
        {
            _context.Machines.Add(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Departments = _context.Departments.OrderBy(d => d.Name).ToList();
        return View(model);
    }
}
