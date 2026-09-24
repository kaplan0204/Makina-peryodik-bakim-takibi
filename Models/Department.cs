namespace MakinaPeryodikBakimTakibi.Models;

public class Department
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ResponsiblePerson { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<Machine> Machines { get; set; } = new List<Machine>();
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}

public class Machine
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;
    public DateTime? ProductionYear { get; set; }
    public DateTime? InstallationDate { get; set; }
    public DateTime? WarrantyStart { get; set; }
    public DateTime? WarrantyEnd { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<MaintenanceRecord> MaintenanceRecords { get; set; } = new List<MaintenanceRecord>();
    public ICollection<PeriodicMaintenance> PeriodicMaintenances { get; set; } = new List<PeriodicMaintenance>();
}

public class Employee
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string EmployeeNumber { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;
    public string Position { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
}

public class MachineOperation
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int EstimatedMinutes { get; set; }
    public bool IsPeriodic { get; set; }
    public int DefaultPeriodDays { get; set; }
    public bool IsActive { get; set; } = true;
}

public class MaintenanceRecord
{
    public int Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public DateTime MaintenanceDate { get; set; }
    public int MachineId { get; set; }
    public Machine Machine { get; set; } = null!;
    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;
    public string Type { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public int? ResponsibleEmployeeId { get; set; }
    public Employee? ResponsibleEmployee { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? NextMaintenanceDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }

    public ICollection<MaintenanceOperation> Operations { get; set; } = new List<MaintenanceOperation>();
    public ICollection<MaintenanceStage> Stages { get; set; } = new List<MaintenanceStage>();
    public ICollection<MaintenanceImage> Images { get; set; } = new List<MaintenanceImage>();
}

public class MaintenanceOperation
{
    public int Id { get; set; }
    public int MaintenanceRecordId { get; set; }
    public MaintenanceRecord MaintenanceRecord { get; set; } = null!;
    public string OperationName { get; set; } = string.Empty;
    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class MaintenanceStage
{
    public int Id { get; set; }
    public int MaintenanceRecordId { get; set; }
    public MaintenanceRecord MaintenanceRecord { get; set; } = null!;
    public int Order { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? EmployeeId { get; set; }
    public Employee? Employee { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class MaintenanceImage
{
    public int Id { get; set; }
    public int MaintenanceRecordId { get; set; }
    public MaintenanceRecord MaintenanceRecord { get; set; } = null!;
    public string FileName { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

public class PeriodicMaintenance
{
    public int Id { get; set; }
    public int MachineId { get; set; }
    public Machine Machine { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public int PeriodDays { get; set; }
    public DateTime LastMaintenanceDate { get; set; }
    public DateTime NextMaintenanceDate { get; set; }
    public bool IsActive { get; set; } = true;
}
