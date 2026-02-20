# QMCH QUICK START GUIDE

## Getting Started with QMCH Phase 1

### Prerequisites
- .NET 10 SDK installed
- SQL Server 2019 or later
- Visual Studio 2022+ or Visual Studio Code
- Git for version control

---

## ?? Quick Setup (5 minutes)

### 1. Clone & Open Project
```bash
cd C:\Users\hp\Desktop\QMCH
# Project is already open in Visual Studio
```

### 2. Database Setup
```powershell
# Open Package Manager Console in Visual Studio
# OR use command line:

cd QMCH
dotnet ef database update

# This will:
# - Create the QMCH database
# - Run all migrations
# - Create 25+ tables with relationships
```

### 3. Configure Connection String
Edit `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=QMCH;Trusted_Connection=true;"
  }
}
```

### 4. Build & Run
```powershell
# Build project
dotnet build

# Run application
dotnet run

# Application will start at:
# https://localhost:5001
# http://localhost:5000
```

### 5. Login
- Default roles created: Admin, Manager, Supervisor, Employee, HR
- Create first admin user through registration (configure in seed data for production)

---

## ?? Understanding the Solution Structure

```
QMCH/                              # Root project folder
??? Models/                        # Domain entities (20+)
?   ??? Patient.cs               # Patient data model
?   ??? Staff.cs                 # Employee/staff model
?   ??? Visit.cs                 # Visit scheduling & documentation
?   ??? SkillModels.cs          # Skills and certifications
?   ??? Schedule.cs              # Shift scheduling
?   ??? TimeAttendance.cs        # Time tracking
?   ??? Compliance.cs            # Licenses & compliance
?   ??? Communication.cs         # Messages & notifications
?   ??? CarePlan.cs              # Care planning
?   ??? Assessment.cs            # Patient assessments
?   ??? Document.cs              # Document storage
?   ??? FinanceAndAudit.cs       # Insurance, claims, audit
?
??? Data/
?   ??? ApplicationDbContext.cs   # EF Core DbContext (25+ DbSets)
?
??? Services/                      # Business logic layer
?   ??? PatientService.cs        # Patient operations
?   ??? StaffService.cs          # Staff operations
?   ??? VisitService.cs          # Visit management
?   ??? AdvancedServices.cs      # Insurance, Claims, Scheduling, etc.
?   ??? CommunicationAndReportingServices.cs  # Messaging, Reports, Audit
?   ??? ... (other services)
?
??? Repositories/                  # Data access layer
?   ??? GenericRepository.cs      # Base CRUD operations
?   ??? PatientRepository.cs      # Patient-specific queries
?   ??? StaffRepository.cs        # Staff-specific queries
?   ??? VisitRepository.cs        # Visit-specific queries
?
??? Interfaces/                    # Abstraction layer
?   ??? Services/
?   ?   ??? IPatientService.cs
?   ?   ??? IAdvancedServices.cs  # 13 service interfaces
?   ?   ??? ...
?   ??? Repositories/
?       ??? IGenericRepository.cs
?       ??? IAdvancedRepositories.cs  # 10+ repository interfaces
?       ??? ...
?
??? Components/
?   ??? Pages/
?   ?   ??? Home.razor           # Dashboard (existing)
?   ?   ??? Calendar.razor       # Scheduling
?   ?   ??? Analytics.razor      # Reports
?   ?   ??? ... (other pages)
?   ??? Layout/
?   ?   ??? MainLayout.razor
?   ?   ??? Sidebar.razor
?   ?   ??? DashboardCard.razor
?   ??? ...
?
??? Migrations/                    # EF Core database migrations
?   ??? 20260220000000_QMCH_Phase1_FullImplementation.cs
?
??? Program.cs                    # DI configuration & startup
??? appsettings.json              # Configuration
??? ... (other files)
```

---

## ?? Key Service Methods

### Patient Service
```csharp
var patientService = serviceProvider.GetRequiredService<IPatientService>();

// Get all patients
var patients = await patientService.GetAllAsync();

// Get single patient
var patient = await patientService.GetByIdAsync(patientId);

// Create patient
var newPatient = await patientService.CreateAsync(patientObject);

// Update patient
var updated = await patientService.UpdateAsync(patientObject);

// Delete patient
await patientService.DeleteAsync(patientId);
```

### Insurance Service
```csharp
var insuranceService = serviceProvider.GetRequiredService<IInsuranceService>();

// Get patient's insurance policies
var insurances = await insuranceService.GetByPatientIdAsync(patientId);

// Create new insurance policy
var policy = await insuranceService.CreateAsync(insuranceObject);
```

### Claim Service
```csharp
var claimService = serviceProvider.GetRequiredService<IClaimService>();

// Get all claims by status
var pendingClaims = await claimService.GetByStatusAsync("Pending");

// Get outstanding claims amount
var outstanding = await claimService.GetOutstandingClaimsAsync();

// Get total revenue
var revenue = await claimService.GetTotalRevenueAsync();
```

### Schedule Service
```csharp
var scheduleService = serviceProvider.GetRequiredService<IScheduleManagementService>();

// Check staff availability
bool available = await scheduleService.IsStaffAvailableAsync(
    staffId, 
    startTime, 
    endTime
);

// Get staff schedule for date range
var schedule = await scheduleService.GetByStaffAndDateRangeAsync(
    staffId, 
    startDate, 
    endDate
);
```

### Reporting Service
```csharp
var reportingService = serviceProvider.GetRequiredService<IReportingService>();

// Get daily census
var census = await reportingService.GetDailyCensusAsync(date);

// Get revenue report
var revenue = await reportingService.GetRevenueReportAsync(startDate, endDate);

// Get staff productivity
var productivity = await reportingService.GetStaffProductivityAsync(startDate, endDate);

// Get license expiry report
var licenses = await reportingService.GetLicenseExpiryReportAsync();
```

---

## ??? Creating New Features

### 1. Add Model
Create new entity in `Models/` folder:
```csharp
public class NewEntity
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
```

### 2. Add to DbContext
In `Data/ApplicationDbContext.cs`:
```csharp
public DbSet<NewEntity> NewEntities { get; set; } = null!;
```

### 3. Create Repository Interface
In `Interfaces/Repositories/`:
```csharp
public interface INewEntityRepository : IGenericRepository<NewEntity>
{
    Task<List<NewEntity>> GetByNameAsync(string name);
}
```

### 4. Create Repository Implementation
In `Repositories/`:
```csharp
public class NewEntityRepository : GenericRepository<NewEntity>, INewEntityRepository
{
    public NewEntityRepository(ApplicationDbContext context) : base(context) { }
    
    public async Task<List<NewEntity>> GetByNameAsync(string name)
    {
        return await _context.NewEntities
            .Where(e => e.Name.Contains(name))
            .ToListAsync();
    }
}
```

### 5. Create Service Interface
In `Interfaces/Services/`:
```csharp
public interface INewEntityService
{
    Task<NewEntity> CreateAsync(NewEntity entity);
    Task<NewEntity> UpdateAsync(NewEntity entity);
    Task<bool> DeleteAsync(int id);
    Task<List<NewEntity>> GetAllAsync();
}
```

### 6. Create Service Implementation
In `Services/`:
```csharp
public class NewEntityService : INewEntityService
{
    private readonly INewEntityRepository _repository;
    
    public NewEntityService(INewEntityRepository repository)
    {
        _repository = repository;
    }
    
    // Implement interface methods
}
```

### 7. Register in DI
In `Program.cs`:
```csharp
builder.Services.AddScoped<INewEntityRepository, NewEntityRepository>();
builder.Services.AddScoped<INewEntityService, NewEntityService>();
```

### 8. Create Migration
```powershell
dotnet ef migrations add AddNewEntity
dotnet ef database update
```

### 9. Create Blazor Page
In `Components/Pages/`:
```razor
@page "/newentity/list"
@inject INewEntityService Service

<PageTitle>New Entities - QMCH</PageTitle>

<h1>New Entities</h1>

@if (entities == null)
{
    <p>Loading...</p>
}
else
{
    <table>
        @foreach (var entity in entities)
        {
            <tr>
                <td>@entity.Name</td>
                <td>@entity.CreatedAt</td>
            </tr>
        }
    </table>
}

@code {
    private List<NewEntity> entities;
    
    protected override async Task OnInitializedAsync()
    {
        entities = await Service.GetAllAsync();
    }
}
```

---

## ?? Testing Services

### Manual Testing in Program.cs
```csharp
using (var scope = app.Services.CreateScope())
{
    var patientService = scope.ServiceProvider.GetRequiredService<IPatientService>();
    
    // Test patient creation
    var patient = new Patient 
    { 
        FirstName = "John",
        LastName = "Doe",
        DateOfBirth = DateTime.Now.AddYears(-30)
    };
    
    await patientService.CreateAsync(patient);
    
    Console.WriteLine("Patient created successfully!");
}
```

---

## ?? Database Queries

### View Database Tables
```sql
-- SQL Server
USE QMCH
GO

-- List all tables
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES

-- View patient data
SELECT * FROM Patients

-- View staff data
SELECT * FROM StaffMembers

-- View visits
SELECT * FROM Visits

-- View claims
SELECT * FROM Claims

-- View audit log
SELECT * FROM AuditLog
```

---

## ?? Debugging Tips

### Enable SQL Query Logging
In `Program.cs`:
```csharp
builder.Services.AddLogging(config =>
{
    config.SetMinimumLevel(LogLevel.Debug);
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString)
           .EnableSensitiveDataLogging() // See parameter values
           .LogTo(Console.WriteLine);      // Log queries
});
```

### Check Entity State
```csharp
var entity = await _context.Patients.FindAsync(id);
var entry = _context.Entry(entity);
Console.WriteLine($"State: {entry.State}"); // Detached, Unchanged, Added, Modified, Deleted
```

---

## ?? Common Tasks

### Get Patient with All Data
```csharp
var patient = await _context.Patients
    .Include(p => p.Visits)
    .Include(p => p.Insurances)
    .Include(p => p.Medications)
    .Include(p => p.Assessments)
    .FirstOrDefaultAsync(p => p.Id == patientId);
```

### Check Staff Availability
```csharp
bool available = await _scheduleService.IsStaffAvailableAsync(
    staffId,
    DateTime.Now.AddHours(2),
    DateTime.Now.AddHours(3)
);
```

### Generate Revenue Report
```csharp
var revenue = await _reportingService.GetRevenueReportAsync(
    DateTime.Now.AddMonths(-1),
    DateTime.Now
);
```

### Track Compliance Status
```csharp
var compliance = await _complianceService.GetByStaffIdAsync(staffId);
var status = compliance.Select(c => new {
    c.ComplianceRequirement.Name,
    c.Status,
    c.ExpiryDate
});
```

---

## ?? Troubleshooting

### Issue: Database not created
**Solution:**
```powershell
dotnet ef database drop
dotnet ef database update
```

### Issue: Migration conflicts
**Solution:**
```powershell
# Remove last migration
dotnet ef migrations remove

# Reapply migrations
dotnet ef database update
```

### Issue: Services not injected
**Solution:**
- Verify service is registered in `Program.cs`
- Check interface/implementation names match
- Verify no circular dependencies

### Issue: Foreign key errors
**Solution:**
- Verify parent entity exists before creating child
- Check cascade delete rules in DbContext
- Verify relationship configuration is correct

---

## ?? Documentation Files

- `IMPLEMENTATION_SUMMARY.md` - Complete Phase 1 overview
- `ARCHITECTURE.md` - System design and database ERD
- `FILES_MANIFEST.md` - All files created/modified
- `REQUIREMENTS_CHECKLIST.md` - Requirements status
- `QUICK_START_GUIDE.md` - This file

---

## ?? Next Steps

1. **Run the application**
   ```powershell
   dotnet run
   ```

2. **Navigate to Home page**
   - Visit `https://localhost:5001`

3. **Create admin user**
   - Register first user (will be admin by default in seed data)

4. **Explore modules**
   - Patient Management
   - Staff Management
   - Scheduling
   - Billing
   - Reports

5. **Begin Phase 2 development**
   - Create Blazor CRUD pages
   - Implement advanced features
   - Build mobile app

---

## ?? Quick Reference

| Component | Location | Purpose |
|-----------|----------|---------|
| DbContext | `Data/ApplicationDbContext.cs` | Database access |
| Services | `Services/` | Business logic |
| Repositories | `Repositories/` | Data queries |
| Models | `Models/` | Domain entities |
| Pages | `Components/Pages/` | UI components |
| Migrations | `Migrations/` | Database schema |

---

**Created:** 2026-02-20
**Phase:** 1.0
**Status:** Ready for Development
