using MakinaPeryodikBakimTakibi.Data;
using MakinaPeryodikBakimTakibi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MakinaPeryodikBakimTakibi.Controllers;

public class EmployeeController : Controller
{
    private readonly ApplicationDbContext _context;

    public EmployeeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var model = _context.Employees
            .Include(e => e.Department)
            .OrderBy(e => e.LastName)
            .ToList();
        return View(model);
    }

    public IActionResult Create()
    {
        ViewBag.Departments = _context.Departments.OrderBy(d => d.Name).ToList();
        return View(new Employee());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Employee model)
    {
        if (ModelState.IsValid)
        {
            _context.Employees.Add(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Departments = _context.Departments.OrderBy(d => d.Name).ToList();
        return View(model);
    }
}
