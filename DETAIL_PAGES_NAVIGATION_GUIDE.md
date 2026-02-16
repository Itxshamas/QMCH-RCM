# Navigation & Usage Guide - Detail Pages

## ?? Page Routes

### Patient Detail Page
```
Route: /hmc/patients/detail/{patientId}
Example: /hmc/patients/detail/1
```

### Staff Detail Page
```
Route: /hmc/staff/detail/{staffId}
Example: /hmc/staff/detail/5
```

---

## ?? Navigation Methods

### From Patient List Page
```html
<!-- Option 1: Direct Link -->
<a href="/hmc/patients/detail/@patient.PatientId" class="btn btn-primary">
    View Details
</a>

<!-- Option 2: Using NavigationManager in code-behind -->
private void ViewPatientDetails(int patientId)
{
    Navigation.NavigateTo($"/hmc/patients/detail/{patientId}");
}
```

### From Staff List Page
```html
<!-- Option 1: Direct Link -->
<a href="/hmc/staff/detail/@staff.Id" class="btn btn-primary">
    View Details
</a>

<!-- Option 2: Using NavigationManager in code-behind -->
private void ViewStaffDetails(int staffId)
{
    Navigation.NavigateTo($"/hmc/staff/detail/{staffId}");
}
```

### From Sidebar Navigation
```html
<a href="/hmc/patients/list">Patients</a>
<a href="/hmc/staff/list">Staff</a>
```

---

## ?? Page Features Breakdown

### Patient Detail Page Features

**Header Section**
- Patient full name
- Patient ID
- Edit button ? Navigates to `/hmc/patients/form/{patientId}`
- Delete button ? Shows confirmation modal

**Information Sections**
1. **Personal Information**
   - Full Name
   - Date of Birth
   - Gender
   - SSN (masked)

2. **Contact Information**
   - Email (clickable)
   - Phone (clickable)
   - Address

3. **Medical Information**
   - Primary Diagnosis
   - Admission Date
   - Status (color-coded badge)

4. **Emergency Contact**
   - Name
   - Phone (clickable)

5. **Tabs**
   - Recent Visits
   - Insurance
   - Medications
   - Care Plan

**Metadata**
- Created timestamp
- Last updated timestamp

---

### Staff Detail Page Features

**Header Section**
- Staff full name
- Role and Staff ID
- Edit button ? Navigates to `/hmc/staff/form/{staffId}`
- Delete button ? Shows confirmation modal

**Information Sections**
1. **Personal Information**
   - Full Name (First, Middle, Last)
   - Date of Birth
   - Gender
   - Staff ID

2. **Professional Information**
   - Role
   - Department
   - Hire Date
   - License Number
   - License Expiry (shows expired status if needed)
   - Hourly Rate (formatted as currency)
   - Status (color-coded badge)
   - Availability (Yes/No badge)

3. **Contact Information**
   - Email (clickable)
   - Phone (clickable)
   - Address

4. **Emergency Contact**
   - Name
   - Phone (clickable)

5. **Tabs**
   - Skills
   - Licenses
   - Schedule
   - Compliance

**Metadata**
- Created timestamp
- Last updated timestamp

---

## ?? UI Components Used

### Status Badges
```html
<!-- Patient Status -->
<span class="badge" style="background-color: #28a745">Active</span>
<span class="badge" style="background-color: #6c757d">Inactive</span>
<span class="badge" style="background-color: #dc3545">Discharged</span>

<!-- Staff Status -->
<span class="badge" style="background-color: #28a745">Active</span>
<span class="badge" style="background-color: #6c757d">Inactive</span>
<span class="badge" style="background-color: #ffc107">On Leave</span>

<!-- Availability Badge -->
<span class="badge" style="background-color: #28a745">Yes</span>
<span class="badge" style="background-color: #6c757d">No</span>
```

### Loading State
```html
<div class="loading-state">
    <div class="spinner"></div>
    <p>Loading staff details...</p>
</div>
```

### Tab Navigation
```html
<div class="tabs-header">
    <button class="tab-btn active" @onclick="...">Tab 1</button>
    <button class="tab-btn" @onclick="...">Tab 2</button>
</div>
<div class="tabs-content">
    <!-- Tab content -->
</div>
```

### Modal Dialog
```html
<div class="modal-overlay">
    <div class="modal-content">
        <h3>Confirm Delete</h3>
        <p>Are you sure you want to delete?</p>
        <div class="modal-actions">
            <button class="btn cancel-btn">Cancel</button>
            <button class="btn confirm-btn">Delete</button>
        </div>
    </div>
</div>
```

---

## ?? Data Flow

### Patient Detail Page Flow
```
1. User navigates to /hmc/patients/detail/1
2. PatientDetail component initializes
3. OnInitializedAsync() called
4. LoadPatientDetails() fetches data via IPatientService
5. UI renders with patient data
6. User can:
   - View all information
   - Click Edit ? navigate to form
   - Click Delete ? show modal
   - Click tabs to view different sections
```

### Staff Detail Page Flow
```
1. User navigates to /hmc/staff/detail/5
2. StaffDetail component initializes
3. OnInitializedAsync() called
4. LoadStaffDetails() fetches data via IStaffService
5. UI renders with staff data
6. User can:
   - View all information
   - Click Edit ? navigate to form
   - Click Delete ? show modal
   - Click tabs to view different sections
```

---

## ?? Integration Points

### With Patient List Page
```csharp
// In PatientList.razor or PatientList.razor.cs
private void ViewPatientDetails(Patient patient)
{
    Navigation.NavigateTo($"/hmc/patients/detail/{patient.PatientId}");
}
```

### With Staff List Page
```csharp
// In StaffList.razor or StaffList.razor.cs
private void ViewStaffDetails(Staff staff)
{
    Navigation.NavigateTo($"/hmc/staff/detail/{staff.Id}");
}
```

### With Sidebar Menu
```html
<!-- Add to Sidebar.razor -->
<a href="/hmc/patients/list" class="nav-link">
    <i class="fa fa-users"></i> Patients
</a>
<a href="/hmc/staff/list" class="nav-link">
    <i class="fa fa-stethoscope"></i> Staff
</a>
```

---

## ?? Access Control

To add role-based access control:

```csharp
[Authorize(Roles = "Admin,Manager")]
public partial class PatientDetail
{
    // ... component code
}

[Authorize(Roles = "Admin,Manager,Staff")]
public partial class StaffDetail
{
    // ... component code
}
```

---

## ? Performance Tips

1. **Lazy Load Tabs** - Load tab content only when clicked
2. **Cache Data** - Consider caching patient/staff info in session
3. **Pagination** - For list items in tabs, use pagination
4. **Async/Await** - All data loading is async to prevent blocking

---

## ?? Error Handling

The detail pages handle:
- Entity not found (displays error message)
- Service errors (logged to console)
- Deletion errors (shows error notification)
- Network timeouts (shows loading state)

---

## ?? Responsive Breakpoints

```css
Desktop:    >= 1200px  (Full layout)
Tablet:     768px-1199px (Adjusted columns)
Mobile:     < 768px    (Single column)
```

---

## ? Testing Scenarios

### Happy Path
1. ? Navigate to patient/staff detail page
2. ? Verify all data displays correctly
3. ? Click tabs and verify content loads
4. ? Click Edit and verify navigation
5. ? Click Delete and verify modal appears

### Edge Cases
1. ? Invalid ID (non-existent entity)
2. ? Missing optional fields
3. ? Long text content
4. ? Mobile viewport
5. ? Slow network (verify loading state)

---

## ?? Support

For issues or customization needs, refer to:
- NurseSearch.razor (original template)
- PatientDetail.razor (patient implementation)
- StaffDetail.razor (staff implementation)

Build Status: ? **SUCCESSFUL**
