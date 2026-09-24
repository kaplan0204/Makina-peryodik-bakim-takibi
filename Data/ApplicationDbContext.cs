using MakinaPeryodikBakimTakibi.Models;
using Microsoft.EntityFrameworkCore;

namespace MakinaPeryodikBakimTakibi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Machine> Machines => Set<Machine>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<MachineOperation> MachineOperations => Set<MachineOperation>();
    public DbSet<MaintenanceRecord> MaintenanceRecords => Set<MaintenanceRecord>();
    public DbSet<MaintenanceOperation> MaintenanceOperations => Set<MaintenanceOperation>();
    public DbSet<MaintenanceStage> MaintenanceStages => Set<MaintenanceStage>();
    public DbSet<MaintenanceImage> MaintenanceImages => Set<MaintenanceImage>();
    public DbSet<PeriodicMaintenance> PeriodicMaintenances => Set<PeriodicMaintenance>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Machine>()
            .HasOne(m => m.Department)
            .WithMany(d => d.Machines)
            .HasForeignKey(m => m.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MaintenanceRecord>()
            .HasOne(m => m.Machine)
            .WithMany(machine => machine.MaintenanceRecords)
            .HasForeignKey(m => m.MachineId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MaintenanceRecord>()
            .HasOne(m => m.ResponsibleEmployee)
            .WithMany()
            .HasForeignKey(m => m.ResponsibleEmployeeId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<MaintenanceOperation>()
            .HasOne(m => m.MaintenanceRecord)
            .WithMany(m => m.Operations)
            .HasForeignKey(m => m.MaintenanceRecordId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MaintenanceStage>()
            .HasOne(m => m.MaintenanceRecord)
            .WithMany(m => m.Stages)
            .HasForeignKey(m => m.MaintenanceRecordId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MaintenanceImage>()
            .HasOne(m => m.MaintenanceRecord)
            .WithMany(m => m.Images)
            .HasForeignKey(m => m.MaintenanceRecordId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<PeriodicMaintenance>()
            .HasOne(p => p.Machine)
            .WithMany(m => m.PeriodicMaintenances)
            .HasForeignKey(p => p.MachineId)
            .OnDelete(DeleteBehavior.Cascade);

        if (!Set<Department>().Any())
        {
            var departments = new[]
            {
                new Department { Code = "B-001", Name = "Üretim", Description = "Üretim bölümü", ResponsiblePerson = "Mehmet Yılmaz", IsActive = true },
                new Department { Code = "B-002", Name = "Bakım", Description = "Bakım bölümü", ResponsiblePerson = "Ayşe Demir", IsActive = true },
                new Department { Code = "B-003", Name = "Kalite", Description = "Kalite kontrol bölümü", ResponsiblePerson = "Eren Kara", IsActive = true }
            };
            Set<Department>().AddRange(departments);
        }

        if (!Set<Machine>().Any())
        {
            var departments = Set<Department>().ToList();
            var machines = new[]
            {
                new Machine { Code = "M-101", Name = "CNC Torna 01", SerialNumber = "CN-0001", Brand = "DMG", Model = "MoriSeiki", DepartmentId = departments[0].Id, ProductionYear = new DateTime(2022, 1, 1), InstallationDate = new DateTime(2022, 2, 15), WarrantyStart = new DateTime(2022, 2, 15), WarrantyEnd = new DateTime(2027, 2, 15), Description = "Torna makinesi", IsActive = true },
                new Machine { Code = "M-102", Name = "Pres 03", SerialNumber = "PR-0007", Brand = "Hydra", Model = "HP-300", DepartmentId = departments[0].Id, ProductionYear = new DateTime(2021, 6, 1), InstallationDate = new DateTime(2021, 7, 10), WarrantyStart = new DateTime(2021, 7, 10), WarrantyEnd = new DateTime(2026, 7, 10), Description = "Basınç presi", IsActive = true },
                new Machine { Code = "M-103", Name = "Kompresör 01", SerialNumber = "KP-0012", Brand = "Atlas", Model = "C-250", DepartmentId = departments[1].Id, ProductionYear = new DateTime(2020, 5, 10), InstallationDate = new DateTime(2020, 9, 5), WarrantyStart = new DateTime(2020, 9, 5), WarrantyEnd = new DateTime(2025, 9, 5), Description = "Kompresör", IsActive = true }
            };
            Set<Machine>().AddRange(machines);
        }

        if (!Set<Employee>().Any())
        {
            var departments = Set<Department>().ToList();
            var employees = new[]
            {
                new Employee { Code = "P-001", FirstName = "Ali", LastName = "Kaya", EmployeeNumber = "1001", Phone = "0500 111 22 33", Email = "ali.kaya@firma.com", DepartmentId = departments[1].Id, Position = "Bakım Sorumlusu", Description = "Bakım planlaması", IsActive = true },
                new Employee { Code = "P-002", FirstName = "Deniz", LastName = "Aydın", EmployeeNumber = "1002", Phone = "0500 222 33 44", Email = "deniz.aydin@firma.com", DepartmentId = departments[0].Id, Position = "Operatör", Description = "Makine kontrol", IsActive = true },
                new Employee { Code = "P-003", FirstName = "Selin", LastName = "Gül", EmployeeNumber = "1003", Phone = "0500 333 44 55", Email = "selin.gul@firma.com", DepartmentId = departments[2].Id, Position = "Kalite Uzmanı", Description = "Kalite kontrol", IsActive = true }
            };
            Set<Employee>().AddRange(employees);
        }

        if (!Set<MachineOperation>().Any())
        {
            var operations = new[]
            {
                new MachineOperation { Code = "IO-001", Name = "Yağ seviyesi kontrolü", Description = "Makine yağ seviyesinin kontrol edilmesi", EstimatedMinutes = 30, IsPeriodic = true, DefaultPeriodDays = 30, IsActive = true },
                new MachineOperation { Code = "IO-002", Name = "Filtre temizliği", Description = "Filtre temizliği ve kontrolü", EstimatedMinutes = 45, IsPeriodic = true, DefaultPeriodDays = 45, IsActive = true },
                new MachineOperation { Code = "IO-003", Name = "Hidrolik kontrolü", Description = "Hidrolik sistem kontrolü", EstimatedMinutes = 60, IsPeriodic = true, DefaultPeriodDays = 60, IsActive = true }
            };
            Set<MachineOperation>().AddRange(operations);
        }

        if (!Set<MaintenanceRecord>().Any())
        {
            var machines = Set<Machine>().ToList();
            var employees = Set<Employee>().ToList();
            var records = new[]
            {
                new MaintenanceRecord { Number = "MF-1001", MaintenanceDate = DateTime.Today.AddDays(-5), MachineId = machines[0].Id, DepartmentId = machines[0].DepartmentId, Type = "Periyodik Bakım", Priority = "Normal", ResponsibleEmployeeId = employees[0].Id, Status = "Tamamlandı", Description = "CNC torna 01 düzenli bakım", NextMaintenanceDate = DateTime.Today.AddDays(25), CreatedAt = DateTime.Today.AddDays(-5), UpdatedAt = DateTime.Today.AddDays(-5) },
                new MaintenanceRecord { Number = "MF-1002", MaintenanceDate = DateTime.Today.AddDays(-12), MachineId = machines[1].Id, DepartmentId = machines[1].DepartmentId, Type = "Arıza Bakımı", Priority = "Yüksek", ResponsibleEmployeeId = employees[0].Id, Status = "Açık", Description = "Pres bakım planı", NextMaintenanceDate = DateTime.Today.AddDays(10), CreatedAt = DateTime.Today.AddDays(-12), UpdatedAt = DateTime.Today.AddDays(-12) }
            };
            Set<MaintenanceRecord>().AddRange(records);
        }

        if (!Set<PeriodicMaintenance>().Any())
        {
            var machines = Set<Machine>().ToList();
            Set<PeriodicMaintenance>().AddRange(new[]
            {
                new PeriodicMaintenance { MachineId = machines[0].Id, Name = "Yağ ve filtre kontrolü", PeriodDays = 30, LastMaintenanceDate = DateTime.Today.AddDays(-10), NextMaintenanceDate = DateTime.Today.AddDays(20), IsActive = true },
                new PeriodicMaintenance { MachineId = machines[1].Id, Name = "Genel kontrol", PeriodDays = 45, LastMaintenanceDate = DateTime.Today.AddDays(-20), NextMaintenanceDate = DateTime.Today.AddDays(-5), IsActive = true },
                new PeriodicMaintenance { MachineId = machines[2].Id, Name = "Filtre kontrolü", PeriodDays = 60, LastMaintenanceDate = DateTime.Today.AddDays(-15), NextMaintenanceDate = DateTime.Today.AddDays(45), IsActive = true }
            });
        }

        SaveChanges();
    }
}
