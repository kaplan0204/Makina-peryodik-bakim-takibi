using MakinaPeryodikBakimTakibi.Data;
using MakinaPeryodikBakimTakibi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MakinaPeryodikBakimTakibi.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var viewModel = new DashboardViewModel
        {
            TotalMachines = _context.Machines.Count(),
            ActiveMachines = _context.Machines.Count(m => m.IsActive),
            TotalEmployees = _context.Employees.Count(),
            OpenMaintenances = _context.MaintenanceRecords.Count(m => m.Status == "Açık" || m.Status == "Devam Ediyor"),
            CompletedMaintenances = _context.MaintenanceRecords.Count(m => m.Status == "Tamamlandı"),
            DelayedPeriodicMaintenances = _context.PeriodicMaintenances.Count(p => p.NextMaintenanceDate < DateTime.Today),
            UpcomingPeriodicMaintenances = _context.PeriodicMaintenances.Count(p => p.NextMaintenanceDate >= DateTime.Today && p.NextMaintenanceDate <= DateTime.Today.AddDays(30)),
            RecentRecords = _context.MaintenanceRecords
                .Include(m => m.Machine)
                .Include(m => m.ResponsibleEmployee)
                .OrderByDescending(m => m.MaintenanceDate)
                .Take(5)
                .ToList(),
            UpcomingList = _context.PeriodicMaintenances
                .Include(p => p.Machine)
                .ThenInclude(m => m.Department)
                .OrderBy(p => p.NextMaintenanceDate)
                .Take(5)
                .ToList(),
            Departments = _context.Departments
                .Include(d => d.Machines)
                .OrderBy(d => d.Name)
                .ToList()
        };

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
