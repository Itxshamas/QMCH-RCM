# PHASE 2 BLAZOR UI COMPONENTS - IMPLEMENTATION GUIDE

## ? COMPLETED PHASE 2 FOUNDATIONS

### 1. Reusable Components Created
- ? **FormInput.razor** - Multi-type form input component (text, email, date, number, select, textarea, checkbox)
- ? **Alert.razor** - Alert/notification component (success, danger, warning, info)
- ? **Loading.razor** - Loading spinner component
- ? **Pagination.razor** - Pagination control
- ? **DataTable.razor** - Generic, sortable, filterable table component
- ? **FileUpload.razor** - File upload with validation

### 2. Data Models Enhanced
- ? User model enhanced with navigation properties (Messages, Notifications, DocumentRecords, AuditLogs)
- ? All migration files synchronized
- ? Database schema ready for Phase 2

### 3. Services Available (from Program.cs)
```
Application Services:
- IPatientService / PatientService
- IStaffService / StaffService
- IVisitService / VisitService
- IUserService / UserService

Advanced Services:
- IInsuranceService / InsuranceService
- IClaimService / ClaimService
- IScheduleManagementService / ScheduleManagementService
- ITimeAttendanceService / TimeAttendanceService
- ITimeOffRequestService / TimeOffRequestService
- ILicenseService / LicenseService
- IComplianceService / ComplianceService
- ICarePlanService / CarePlanService
- IAssessmentService / AssessmentService
- IReportingService / ReportingService
- ICommunicationService / CommunicationService
- IDocumentService / DocumentService
- IAuditService / AuditService
```

---

## ?? NEXT STEPS FOR PHASE 2

### **BLOCK 1: Patient Management Pages** (High Priority)
Create these pages in `Components/Pages/HMC/Patients/`:

1. **PatientDetail.razor** - Complete patient profile view
   - Patient demographics, medical history
   - Current medications, allergies
   - Contact information
   - Recent visits tab
   - Actions: Edit, Add Visit, View Care Plan

2. **PatientInsurance.razor** - Insurance management
   - List insurance policies
   - Add/Edit insurance
   - Upload documents
   - Verify coverage

3. **PatientMedications.razor** - Medication management
   - Current medications
   - Medication history
   - Add/Edit medication
   - Refill requests

4. **PatientAssessments.razor** - Patient assessments
   - Assessment history
   - Create new assessment
   - View assessment details
   - Generate PDF reports

5. **PatientCarePlan.razor** - Care plan management
   - View active care plans
   - Create care plan
   - Track interventions
   - Monitor outcomes

### **BLOCK 2: Staff Management Pages** (High Priority)
Create these pages in `Components/Pages/HMC/Staff/`:

1. **StaffDetail.razor** - Staff profile view
   - Personal information
   - Licenses and certifications
   - Performance metrics
   - Schedule view
   - Activity log

2. **StaffSkills.razor** - Manage staff skills
   - Current skills list
   - Add new skill
   - Certification tracking
   - Skill validation

3. **StaffLicenses.razor** - License management
   - Active licenses
   - Expiration alerts
   - Upload license copies
   - Renewal tracking

4. **StaffSchedule.razor** - Staff scheduling
   - View staff schedule
   - Create/edit shifts
   - Handle conflicts
   - Time off requests

5. **StaffTimeTracking.razor** - Time & attendance
   - Clock in/out interface
   - Attendance history
   - Hours summary
   - Overtime tracking

### **BLOCK 3: Scheduling & Visits** (High Priority)
Create these pages in `Components/Pages/HMC/Visits/`:

1. **VisitList.razor** - List all visits with filters
   - Search by patient, staff, date
   - Status filtering
   - Pagination
   - Quick actions

2. **VisitForm.razor** - Create/Edit visit
   - Schedule visit
   - Assign staff
   - Set time
   - Add notes

3. **VisitDocumentation.razor** - Document visit
   - Vital signs entry
   - Clinical notes
   - Medications administered
   - Patient response
   - Digital signature

4. **ScheduleCalendar.razor** - Enhanced calendar view
   - Visual schedule
   - Drag-and-drop
   - Conflict detection
   - Legend

### **BLOCK 4: Billing & Claims** (Medium Priority)
Create these pages in `Components/Pages/HMC/Billing/`:

1. **InsuranceList.razor** - Manage insurance
   - List insurance policies
   - Add/Edit insurance
   - Verify coverage

2. **ClaimList.razor** - Claims management
   - List all claims
   - Filter by status
   - Search
   - Quick actions

3. **ClaimForm.razor** - Create/Edit claim
   - Claim details
   - Line items
   - Attach documents
   - Submit

4. **BillingDashboard.razor** - Billing overview
   - Revenue summary
   - Outstanding amounts
   - Aging report
   - Charts

### **BLOCK 5: Compliance & Quality** (Medium Priority)
Create these pages in `Components/Pages/HMC/Compliance/`:

1. **ComplianceList.razor** - Staff compliance status
2. **LicenseExpiry.razor** - License expiration tracking
3. **QualityMetrics.razor** - QA metrics dashboard

### **BLOCK 6: Reports & Analytics** (Medium Priority)
Create these pages in `Components/Pages/Reports/`:

1. **ReportsDashboard.razor** - Reports home page
2. **PatientCensusReport.razor** - Daily census
3. **StaffProductivityReport.razor** - Productivity metrics
4. **FinancialReport.razor** - Revenue analysis
5. **OperationalMetrics.razor** - KPIs

### **BLOCK 7: Communication** (Lower Priority)
Create these pages in `Components/Pages/HMC/Communication/`:

1. **MessageList.razor** - Message inbox
2. **MessageForm.razor** - Compose message
3. **NotificationCenter.razor** - Notifications

---

## ??? COMPONENT PATTERNS

### Basic Blazor Page Template
```razor
@page "/path"
@inject IService Service
@rendermode InteractiveServer

<PageTitle>Page Title - QMCH</PageTitle>

<div class="container-fluid py-4">
    <!-- Header -->
    <div class="page-header mb-4">
        <h1 class="h3 mb-0">Page Title</h1>
    </div>

    <!-- Loading State -->
    @if (IsLoading)
    {
        <Loading IsVisible="true" />
    }
    else if (Data != null && Data.Count > 0)
    {
        <!-- Content -->
    }
    else
    {
        <Alert Type="info" Title="No Data" Message="No records found" />
    }
</div>

@code {
    private bool IsLoading = true;
    private List<Model> Data = new();

    protected override async Task OnInitializedAsync()
    {
        await LoadData();
    }

    private async Task LoadData()
    {
        IsLoading = true;
        try
        {
            Data = await Service.GetAllAsync();
        }
        finally
        {
            IsLoading = false;
        }
    }
}
```

### Form Binding Pattern
```razor
<EditForm Model="Model" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />
    <ValidationSummary />

    <FormInput Label="Name" @bind-Value="Model.Name" Required="true" />
    <FormInput Label="Email" Type="email" @bind-Value="Model.Email" />
    <FormInput Label="Type" Type="select" @bind-Value="Model.Type" 
              Options="TypeOptions" />
    <FormInput Label="Description" Type="textarea" @bind-Value="Model.Description" />

    <button type="submit" class="btn btn-primary">Submit</button>
</EditForm>

@code {
    private Model Model = new();
    private Dictionary<string, string> TypeOptions = new()
    {
        { "type1", "Type 1" },
        { "type2", "Type 2" }
    };

    private async Task HandleSubmit()
    {
        await Service.SaveAsync(Model);
    }
}
```

### Table & Pagination Pattern
```razor
<div class="card">
    <div class="table-responsive">
        <table class="table table-striped">
            <thead>
                <tr>
                    <th @onclick="@(() => Sort("Name"))">Name</th>
                    <th>Status</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                @foreach (var item in PaginatedItems)
                {
                    <tr>
                        <td>@item.Name</td>
                        <td>@item.Status</td>
                        <td>
                            <button @onclick="@(() => Edit(item.Id))">Edit</button>
                            <button @onclick="@(() => Delete(item.Id))">Delete</button>
                        </td>
                    </tr>
                }
            </tbody>
        </table>
    </div>
    <Pagination CurrentPage="CurrentPage" PageSize="10" TotalItems="Total"
                OnPageChanged="@((p) => CurrentPage = p)" />
</div>

@code {
    private int CurrentPage = 1;
    private int PageSize = 10;
    private List<Item> Items = new();
    private List<Item> PaginatedItems => Items.Skip((CurrentPage - 1) * PageSize)
                                               .Take(PageSize).ToList();
}
```

---

## ?? STYLING & BOOTSTRAP CLASSES

### Card Header
```razor
<div class="card">
    <div class="card-header bg-light">
        <h5 class="mb-0">Section Title</h5>
    </div>
    <div class="card-body">
        <!-- Content -->
    </div>
</div>
```

### Status Badges
```razor
@foreach (var item in Items)
{
    <span class="badge bg-@GetStatusColor(item.Status)">@item.Status</span>
}

@code {
    private string GetStatusColor(string status) => status switch
    {
        "Active" => "success",
        "Inactive" => "secondary",
        "Pending" => "warning",
        "Failed" => "danger",
        _ => "info"
    };
}
```

### Action Buttons
```razor
<div class="btn-group btn-group-sm" role="group">
    <button class="btn btn-info" @onclick="@(() => View(item.Id))">
        <i class="bi bi-eye"></i> View
    </button>
    <button class="btn btn-primary" @onclick="@(() => Edit(item.Id))">
        <i class="bi bi-pencil"></i> Edit
    </button>
    <button class="btn btn-danger" @onclick="@(() => Delete(item.Id))">
        <i class="bi bi-trash"></i> Delete
    </button>
</div>
```

---

## ?? COMPONENT IMPORTS (add to shared Imports.razor)

```razor
@using QMCH.Components.Shared
@using QMCH.Models
@using QMCH.Interfaces.Services
@using QMCH.Services
@using System.ComponentModel.DataAnnotations
```

---

## ?? SERVICE INJECTION PATTERN

```csharp
@inject IPatientService PatientService
@inject IStaffService StaffService
@inject IVisitService VisitService
@inject NavigationManager Navigation
@inject IJSRuntime JS
```

---

## ? PRIORITY EXECUTION ORDER

1. **Week 1:**
   - PatientDetail.razor
   - PatientList.razor (enhance)
   - PatientForm.razor (enhance)
   - StaffList.razor (enhance)

2. **Week 2:**
   - VisitList.razor
   - VisitForm.razor
   - VisitDocumentation.razor
   - ScheduleCalendar.razor

3. **Week 3:**
   - Billing pages
   - Compliance pages
   - Report pages

4. **Week 4:**
   - Dashboard page
   - Communication pages
   - Polish & optimization

---

## ?? BUILD & TEST CYCLE

After creating each page:

1. Build project: `run_build`
2. Test page routing
3. Test service integration
4. Test form validation
5. Test error handling
6. Commit to git

---

**Ready to build Phase 2 Pages!**
