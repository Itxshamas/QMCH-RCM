# ? QUICK REFERENCE: PAGE REFACTORING
## Copy-Paste Templates & Fast Track Guide

---

## ?? TL;DR - 5 Minutes to Understand

### **The 3-Part Rule**
Every page needs THREE files:

```
MyPage.razor              ? UI ONLY (no code, no styles)
MyPage.razor.cs           ? LOGIC ONLY (code-behind)
MyPage.razor.css          ? STYLES ONLY (responsive)
```

### **The Pattern**

**MyPage.razor** - KEEP IT SIMPLE
```razor
@page "/my-path"
@inject IService Service
@rendermode InteractiveServer

<div class="page-container">
    <!-- JUST MARKUP HERE -->
    <h1>@Title</h1>
    <button @onclick="DoThing">Click</button>
</div>

@code {
    // Parameters only - NO OTHER CODE!
    [Parameter] public string? Id { get; set; }
}
```

**MyPage.razor.cs** - ALL THE LOGIC
```csharp
namespace QMCH.Components.Pages;

public partial class MyPage
{
    [Inject] protected IService? Service { get; set; }
    
    protected string Title = "My Page";
    
    protected override async Task OnInitializedAsync()
    {
        // Load data
    }
    
    protected void DoThing()
    {
        // Handle clicks
    }
}
```

**MyPage.razor.css** - MOBILE & DESKTOP
```css
.page-container {
    padding: 20px;
}

/* Tablet */
@media (max-width: 768px) {
    .page-container { padding: 16px; }
}

/* Mobile */
@media (max-width: 576px) {
    .page-container { padding: 12px; }
}
```

---

## ?? REFACTORING CHECKLIST (5 Steps)

### **Step 1: Audit (2 min)**
```bash
# Check if files exist
ls -la Components/Pages/MyPage.*

# You need:
? MyPage.razor
? MyPage.razor.cs (or create it)
? MyPage.razor.css (or create it)
```

### **Step 2: Extract Logic (5 min)**
If this is in MyPage.razor:
```razor
@code {
    private string name;
    protected override async Task OnInitializedAsync() { }
    private void DoSomething() { }
}
```

MOVE it here in MyPage.razor.cs:
```csharp
public partial class MyPage
{
    private string name;
    protected override async Task OnInitializedAsync() { }
    private void DoSomething() { }
}
```

### **Step 3: Extract Styles (5 min)**
If this is in MyPage.razor:
```razor
<style>
    .my-class { color: red; }
</style>
```

MOVE it here in MyPage.razor.css:
```css
.my-class { color: red; }
```

### **Step 4: Clean MyPage.razor (5 min)**
REMOVE:
- ? `@code { }` block
- ? `<style>` tag
- ? Logic/event handlers

KEEP:
- ? `@page` directive
- ? `@inject` directives
- ? HTML/markup only
- ? `@rendermode`
- ? Parameter definitions

### **Step 5: Add Responsiveness (5 min)**
In MyPage.razor.css add:
```css
/* Desktop (base styles above) */

/* Tablet - max-width 768px */
@media (max-width: 768px) {
    /* Tablet-specific styles */
}

/* Mobile - max-width 576px */
@media (max-width: 576px) {
    /* Mobile-specific styles */
}
```

---

## ?? COPY-PASTE TEMPLATES

### **Template 1: Simple List Page**

**MyList.razor.cs**
```csharp
using Microsoft.AspNetCore.Components;
using QMCH.Interfaces.Services;

namespace QMCH.Components.Pages
{
    public partial class MyList
    {
        [Inject] protected IMyService? Service { get; set; }
        [Inject] protected NavigationManager? Navigation { get; set; }

        private List<MyItem>? items;
        private string searchTerm = "";
        private bool isLoading = true;
        private bool hasError = false;
        private string errorMessage = "";

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                isLoading = true;
                items = await Service?.GetItemsAsync() ?? new();
                isLoading = false;
            }
            catch (Exception ex)
            {
                hasError = true;
                errorMessage = ex.Message;
                isLoading = false;
            }
        }

        private void CreateNew() => Navigation?.NavigateTo("/my-path/create");
        private void ViewItem(int id) => Navigation?.NavigateTo($"/my-path/{id}");
        private void EditItem(int id) => Navigation?.NavigateTo($"/my-path/edit/{id}");

        private async Task DeleteItem(int id)
        {
            try
            {
                await Service?.DeleteItemAsync(id)!;
                await LoadData();
            }
            catch (Exception ex)
            {
                hasError = true;
                errorMessage = ex.Message;
            }
        }
    }
}
```

**MyList.razor**
```razor
@page "/my-path/list"
@inject IMyService Service
@inject NavigationManager Navigation
@rendermode InteractiveServer

<PageTitle>My Items - App</PageTitle>

@if (hasError)
{
    <div class="alert alert-danger">@errorMessage</div>
}

@if (isLoading)
{
    <div class="text-center"><p>Loading...</p></div>
}
else
{
    <div class="page-header">
        <h1>My Items</h1>
        <button class="btn btn-primary" @onclick="CreateNew">+ New</button>
    </div>

    <input type="text" placeholder="Search..." @bind="searchTerm" />

    <table class="table">
        <thead><tr><th>Name</th><th>Actions</th></tr></thead>
        <tbody>
            @foreach (var item in items?.Where(x => x.Name.Contains(searchTerm)) ?? [])
            {
                <tr>
                    <td>@item.Name</td>
                    <td>
                        <button @onclick="() => ViewItem(item.Id)">View</button>
                        <button @onclick="() => EditItem(item.Id)">Edit</button>
                        <button @onclick="() => DeleteItem(item.Id)">Delete</button>
                    </td>
                </tr>
            }
        </tbody>
    </table>
}

@code {
    [Parameter] public string? Status { get; set; }
}
```

**MyList.razor.css**
```css
.page-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 20px;
}

.page-header h1 {
    margin: 0;
    font-size: 32px;
}

.table {
    width: 100%;
    border-collapse: collapse;
}

.table thead {
    background: #f8f9fa;
}

.table th, .table td {
    padding: 12px;
    border-bottom: 1px solid #dee2e6;
}

.table tbody tr:hover {
    background: #f8f9fa;
}

.btn {
    padding: 8px 12px;
    margin-right: 4px;
    cursor: pointer;
    border-radius: 4px;
}

/* Responsive */
@media (max-width: 768px) {
    .table {
        font-size: 14px;
    }
    
    .table th, .table td {
        padding: 8px;
    }
}

@media (max-width: 576px) {
    .page-header {
        flex-direction: column;
        align-items: flex-start;
    }
    
    .page-header button {
        width: 100%;
        margin-top: 10px;
    }
    
    .table {
        display: block;
        overflow-x: auto;
    }
}
```

---

### **Template 2: Form Page**

**MyForm.razor.cs**
```csharp
using Microsoft.AspNetCore.Components;
using QMCH.Interfaces.Services;

namespace QMCH.Components.Pages
{
    public partial class MyForm
    {
        [Inject] protected IMyService? Service { get; set; }
        [Inject] protected NavigationManager? Navigation { get; set; }

        [Parameter] public int? Id { get; set; }

        private MyItem? item = new();
        private bool isSaving = false;
        private bool hasError = false;
        private string errorMessage = "";

        protected override async Task OnInitializedAsync()
        {
            if (Id.HasValue)
            {
                item = await Service?.GetItemAsync(Id.Value)!;
            }
        }

        private async Task SaveItem()
        {
            try
            {
                isSaving = true;
                if (item?.Id == 0)
                    await Service?.CreateItemAsync(item)!;
                else
                    await Service?.UpdateItemAsync(item)!;
                Navigation?.NavigateTo("/my-path/list");
            }
            catch (Exception ex)
            {
                hasError = true;
                errorMessage = ex.Message;
                isSaving = false;
            }
        }

        private void Cancel() => Navigation?.NavigateTo("/my-path/list");
    }
}
```

**MyForm.razor**
```razor
@page "/my-path/create"
@page "/my-path/edit/{id:int}"
@inject IMyService Service
@inject NavigationManager Navigation
@rendermode InteractiveServer

<PageTitle>@(Id.HasValue ? "Edit" : "Create") Item</PageTitle>

@if (hasError)
{
    <div class="alert alert-danger">@errorMessage</div>
}

<div class="form-container">
    <h1>@(Id.HasValue ? "Edit" : "Create") Item</h1>

    <form @onsubmit="SaveItem">
        <div class="form-group">
            <label>Name:</label>
            <input type="text" class="form-control" @bind="item!.Name" required />
        </div>

        <div class="form-group">
            <label>Description:</label>
            <textarea class="form-control" @bind="item!.Description"></textarea>
        </div>

        <div class="form-actions">
            <button type="submit" class="btn btn-primary" disabled="@isSaving">
                @(isSaving ? "Saving..." : "Save")
            </button>
            <button type="button" class="btn btn-secondary" @onclick="Cancel">
                Cancel
            </button>
        </div>
    </form>
</div>

@code {
    [Parameter] public int? Id { get; set; }
}
```

**MyForm.razor.css**
```css
.form-container {
    max-width: 600px;
    margin: 0 auto;
    padding: 20px;
}

.form-group {
    margin-bottom: 20px;
}

.form-group label {
    display: block;
    margin-bottom: 8px;
    font-weight: 500;
}

.form-control {
    width: 100%;
    padding: 8px 12px;
    border: 1px solid #dee2e6;
    border-radius: 4px;
    font-size: 14px;
}

.form-control:focus {
    outline: none;
    border-color: #0066cc;
    box-shadow: 0 0 0 3px rgba(0, 102, 204, 0.1);
}

.form-actions {
    display: flex;
    gap: 10px;
    margin-top: 20px;
}

.btn {
    padding: 10px 20px;
    border: none;
    border-radius: 4px;
    cursor: pointer;
    font-weight: 500;
    transition: all 0.2s;
}

.btn-primary {
    background: #0066cc;
    color: white;
}

.btn-primary:hover:not(:disabled) {
    background: #0052a3;
}

.btn-secondary {
    background: #6c757d;
    color: white;
}

/* Responsive */
@media (max-width: 576px) {
    .form-container {
        padding: 12px;
    }

    .form-actions {
        flex-direction: column;
    }

    .form-control {
        font-size: 16px; /* Prevent iOS zoom */
    }
}
```

---

## ?? TESTING CHECKLIST (2 min)

Before committing:

```
[ ] Page loads without error
[ ] Layout looks good at 1920px
[ ] Layout looks good at 768px (tablet)
[ ] Layout looks good at 375px (phone)
[ ] Search/filter works
[ ] Buttons work
[ ] Error messages show
[ ] Loading spinner shows
[ ] No horizontal scrolling
[ ] Touch targets are big (?44px)
[ ] Forms are readable on phone
```

---

## ?? COMMON MISTAKES

### ? WRONG
```razor
@page "/mypage"

@code {
    private List<Item> items;
    private async Task LoadItems() { }
    
    public void SomeMethod() { }
}

<style>
    .my-class { color: red; }
</style>

<div>@item.Name</div>
```

### ? RIGHT
```razor
@page "/mypage"

<div>@item.Name</div>

@code {
    [Parameter] public string? Id { get; set; }
}
```

With MyPage.razor.cs:
```csharp
public partial class MyPage
{
    private List<Item> items;
    protected async Task LoadItems() { }
}
```

With MyPage.razor.css:
```css
.my-class { color: red; }
```

---

## ? COMMANDS

```bash
# Check if files exist
ls -la Components/Pages/MyPage.*

# Create new .cs file (if missing)
# File > New > C# Class
# Name: MyPage.razor.cs
# Content: public partial class MyPage { }

# Create new .css file (if missing)
# File > New > CSS Stylesheet
# Name: MyPage.razor.css
# Content: (add responsive styles)

# Build to check for errors
dotnet build

# Run dev server
dotnet run
```

---

## ?? WHEN IN DOUBT

1. Look at **PatientList.razor** - it's the template
2. Read **PAGE_REFACTORING_GUIDE.md**
3. Follow the 3-Part Rule
4. Test at 3 sizes: 1920px, 768px, 375px

---

**That's it! You're ready to refactor pages.**

