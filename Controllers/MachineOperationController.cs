using MakinaPeryodikBakimTakibi.Data;
using MakinaPeryodikBakimTakibi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MakinaPeryodikBakimTakibi.Controllers;

public class MachineOperationController : Controller
{
    private readonly ApplicationDbContext _context;

    public MachineOperationController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var model = _context.MachineOperations.OrderBy(m => m.Name).ToList();
        return View(model);
    }

    public IActionResult Create()
    {
        return View(new MachineOperation());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(MachineOperation model)
    {
        if (ModelState.IsValid)
        {
            _context.MachineOperations.Add(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        return View(model);
    }
}
