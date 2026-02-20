# Detail Pages - Quick Reference Guide

## Pages Created ?

### 1. **Patient Detail Page**
- **URL:** `/hmc/patients/detail/{patientId}`
- **Files:**
  - `PatientDetail.razor` - UI
  - `PatientDetail.razor.cs` - Logic
  - `PatientDetail.razor.css` - Styling
- **Color Scheme:** Blue (#0066cc)
- **Sections:** Personal Info, Contact, Medical, Emergency Contact, Tabs
- **Status:** ? Build Successful

### 2. **Staff Detail Page**  
- **URL:** `/hmc/staff/detail/{staffId}`
- **Files:**
  - `StaffDetail.razor` - UI
  - `StaffDetail.razor.cs` - Logic
  - `StaffDetail.razor.css` - Styling
- **Color Scheme:** Green (#00a86b)
- **Sections:** Personal Info, Professional, Contact, Emergency Contact, Tabs
- **Status:** ? Build Successful

---

## Template Pattern Applied ?

All pages follow the **NurseSearch.razor** pattern exactly:

```
Component File Structure:
??? .razor          ? Markup & UI elements
??? .razor.cs       ? Logic & State management
??? .razor.css      ? Styling & Animations
```

### Common Features:

? **Page Header**
- Gradient background with primary color
- Entity name and identifier
- Action buttons (Edit, Delete)

? **Content Sections**
- Multiple organized information sections
- Clean grid layout
- Consistent styling

? **Tabbed Interface**
- Multiple content tabs
- Smooth transitions
- Active state styling

? **User Interactions**
- Modal confirmations for destructive actions
- Loading states with spinners
- Navigation between pages

? **Responsive Design**
- Mobile-friendly layouts
- Flexible grid systems
- Touch-friendly buttons

---

## How to Use

### Navigate to Pages:
```csharp
// In your Blazor components:
<a href="/hmc/patients/detail/1">View Patient</a>
<a href="/hmc/staff/detail/5">View Staff Member</a>
```

### Link from List Pages:
```html
<a href="/hmc/patients/detail/@patient.PatientId">View Details</a>
<a href="/hmc/staff/detail/@staff.Id">View Details</a>
```

---

## Styling Guide

### Patient Detail (Blue Theme)
```css
Primary Color: #0066cc
Gradient: linear-gradient(135deg, #0066cc 0%, #004999 100%)
Accent: #003d7a
```

### Staff Detail (Green Theme)
```css
Primary Color: #00a86b
Gradient: linear-gradient(135deg, #00a86b 0%, #007d52 100%)
Accent: #005c3f
```

### Status Colors (Both)
```css
Active:       #28a745 (Green)
Inactive:     #6c757d (Gray)
Discharged:   #dc3545 (Red)
On Leave:     #ffc107 (Yellow)
```

---

## Key Methods

### PatientDetail.razor.cs
```csharp
LoadPatientDetails()         // Fetch patient data
NavigateToEdit()             // Go to edit page
ConfirmDelete()              // Show delete modal
ConfirmDeleteAction()        // Execute delete
GetStatusColor(status)       // Get color for status
```

### StaffDetail.razor.cs
```csharp
LoadStaffDetails()           // Fetch staff data
NavigateToEdit()             // Go to edit page
ConfirmDelete()              // Show delete modal
ConfirmDeleteAction()        // Execute delete
GetStatusColor(status)       // Get color for status
```

---

## Service Methods Used

### IPatientService
- `GetPatientByIdAsync(int id)` - Fetch single patient
- `DeletePatientAsync(int id)` - Delete patient

### IStaffService
- `GetStaffByIdAsync(int id)` - Fetch single staff member
- `DeleteStaffAsync(int id)` - Delete staff member

---

## Extending the Pattern

To create more detail pages following this pattern:

### Step 1: Create .razor file
```razor
@page "/hmc/entity/detail/{entityId:int}"
@rendermode InteractiveServer
<!-- Copy PatientDetail or StaffDetail structure -->
```

### Step 2: Create .razor.cs file
```csharp
public partial class EntityDetail
{
    [Parameter] public int EntityId { get; set; }
    [Inject] private IEntityService Service { get; set; }
    [Inject] private NavigationManager Navigation { get; set; }
    
    private Entity? entity;
    // ... rest of code-behind
}
```

### Step 3: Create .razor.css file
```css
.page-header { /* Copy from PatientDetail or StaffDetail */ }
.detail-section { /* ... */ }
/* Customize colors as needed */
```

---

## Testing Checklist

- ? Navigate to `/hmc/patients/detail/1`
- ? Navigate to `/hmc/staff/detail/1`
- ? Click Edit button (will navigate to form page)
- ? Click Delete button (shows confirmation modal)
- ? Click tabs to view different content
- ? Test responsive design on mobile
- ? Verify loading state appears briefly
- ? Check error handling for missing entities

---

## Build Status: ? SUCCESSFUL

All files compile without errors and are ready to use!
