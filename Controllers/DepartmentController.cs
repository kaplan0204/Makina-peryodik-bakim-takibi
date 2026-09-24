using MakinaPeryodikBakimTakibi.Data;
using MakinaPeryodikBakimTakibi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MakinaPeryodikBakimTakibi.Controllers;

public class DepartmentController : Controller
{
    private readonly ApplicationDbContext _context;

    public DepartmentController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var model = _context.Departments.OrderBy(d => d.Name).ToList();
        return View(model);
    }

    public IActionResult Create()
    {
        return View(new Department());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Department model)
    {
        if (ModelState.IsValid)
        {
            _context.Departments.Add(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }
}
