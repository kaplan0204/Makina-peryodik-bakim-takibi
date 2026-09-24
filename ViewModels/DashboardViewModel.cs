namespace MakinaPeryodikBakimTakibi.ViewModels;

using MakinaPeryodikBakimTakibi.Models;

public class DashboardViewModel
{
    public int TotalMachines { get; set; }
    public int ActiveMachines { get; set; }
    public int TotalEmployees { get; set; }
    public int OpenMaintenances { get; set; }
    public int CompletedMaintenances { get; set; }
    public int DelayedPeriodicMaintenances { get; set; }
    public int UpcomingPeriodicMaintenances { get; set; }
    public List<MaintenanceRecord> RecentRecords { get; set; } = new();
    public List<PeriodicMaintenance> UpcomingList { get; set; } = new();
    public List<Department> Departments { get; set; } = new();
}
