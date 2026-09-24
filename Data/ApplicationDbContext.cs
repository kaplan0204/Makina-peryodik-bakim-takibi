using MakinaPeryodikBakimTakibi.Models;
using Microsoft.EntityFrameworkCore;

namespace MakinaPeryodikBakimTakibi.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
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
    }
}
