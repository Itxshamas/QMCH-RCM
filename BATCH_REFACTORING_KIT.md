# ?? BATCH REFACTORING IMPLEMENTATION KIT
## Multi-Page Template & Quick Setup Guide

---

## ?? READY-TO-USE TEMPLATES

### **Use these exact templates to speed up refactoring**

---

## 1?? LIST PAGE TEMPLATE

Use this for: EmployeeList, ClientList, Applicants, Classifications, etc.

### **Template: YourList.razor.cs**

```csharp
using Microsoft.AspNetCore.Components;
using QMCH.Interfaces.Services;

namespace QMCH.Components.Pages
{
    public partial class YourList
    {
        [Inject] protected IYourService? Service { get; set; }
        [Inject] protected NavigationManager? Navigation { get; set; }

        private List<YourItem>? items;
        private List<YourItem>? filteredItems;
        private string searchTerm = "";
        private bool isLoading = true;
        private bool hasError = false;
        private string errorMessage = "";
        private int totalCount = 0;
        private int activeCount = 0;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                isLoading = true;
                hasError = false;

                if (Service == null)
                    throw new InvalidOperationException("Service is not injected");

                items = await Service.GetAllAsync();
                totalCount = items?.Count() ?? 0;
                activeCount = items?.Count(x => x.IsActive) ?? 0;
                ApplyFilters();

                isLoading = false;
            }
            catch (Exception ex)
            {
                hasError = true;
                errorMessage = $"Failed to load data: {ex.Message}";
                isLoading = false;
            }
        }

        private void ApplyFilters()
        {
            if (items == null) return;

            filteredItems = items.Where(x =>
                string.IsNullOrEmpty(searchTerm) ||
                x.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            ).ToList();
        }

        private void CreateNew()
        {
            if (Navigation != null)
                Navigation.NavigateTo("/your-path/create");
        }

        private void ViewItem(int id)
        {
            if (Navigation != null)
                Navigation.NavigateTo($"/your-path/{id}");
        }

        private void EditItem(int id)
        {
            if (Navigation != null)
                Navigation.NavigateTo($"/your-path/edit/{id}");
        }

        private async Task DeleteItem(int id)
        {
            try
            {
                if (Service != null)
                {
                    await Service.DeleteAsync(id);
                    await LoadData();
                }
            }
            catch (Exception ex)
            {
                hasError = true;
                errorMessage = $"Failed to delete item: {ex.Message}";
            }
        }
    }
}
```

### **Template: YourList.razor**

```razor
@page "/your-path/list"
@inject IYourService Service
@inject NavigationManager Navigation
@rendermode InteractiveServer

<PageTitle>Your Items - QMCH</PageTitle>

@if (hasError)
{
    <div class="alert alert-danger alert-dismissible fade show" role="alert">
        <strong>Error:</strong> @errorMessage
        <button type="button" class="btn-close" @onclick="@(() => hasError = false)" aria-label="Close"></button>
    </div>
}

<div class="list-page">
    <!-- Page Header -->
    <div class="page-header">
        <div class="header-left">
            <h1>YOUR ITEMS</h1>
            <nav aria-label="breadcrumb">
                <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="/">Home</a></li>
                    <li class="breadcrumb-item active">Your Items</li>
                </ol>
            </nav>
        </div>
        <div class="header-right">
            <button class="btn btn-primary" @onclick="CreateNew" aria-label="Create new item">
                <i class="fas fa-plus" aria-hidden="true"></i> NEW
            </button>
        </div>
    </div>

    <!-- Stats Cards -->
    <div class="content-container">
        <div class="stats-row">
            <div class="stat-card">
                <div class="stat-icon">
                    <i class="fas fa-list" aria-hidden="true"></i>
                </div>
                <div class="stat-info">
                    <span class="stat-label">Total</span>
                    <span class="stat-value">@totalCount</span>
                </div>
            </div>
            <div class="stat-card">
                <div class="stat-icon">
                    <i class="fas fa-check-circle" aria-hidden="true"></i>
                </div>
                <div class="stat-info">
                    <span class="stat-label">Active</span>
                    <span class="stat-value">@activeCount</span>
                </div>
            </div>
        </div>

        <!-- Filter Card -->
        <div class="filter-card">
            <div class="search-box">
                <i class="fas fa-search" aria-hidden="true"></i>
                <input type="text" placeholder="Search..." @bind="searchTerm" @bind:event="oninput"
                    @bind:after="ApplyFilters" aria-label="Search items" />
            </div>
        </div>

        <!-- Table Container -->
        <div class="table-container">
            @if (isLoading)
            {
                <div class="text-center py-5">
                    <div class="spinner-border text-primary" role="status">
                        <span class="visually-hidden">Loading...</span>
                    </div>
                    <p class="mt-2">Loading items...</p>
                </div>
            }
            else if (filteredItems == null || !filteredItems.Any())
            {
                <div class="text-center py-5">
                    <i class="fas fa-inbox fa-3x text-muted mb-3" aria-hidden="true"></i>
                    <p>No items found.</p>
                </div>
            }
            else
            {
                <table class="table custom-table" role="table">
                    <thead>
                        <tr>
                            <th scope="col">Name</th>
                            <th scope="col">Status</th>
                            <th scope="col">Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        @foreach (var item in filteredItems)
                        {
                            <tr>
                                <td data-label="Name">@item.Name</td>
                                <td data-label="Status">
                                    <span class="status-badge @(item.IsActive ? "active" : "inactive")">
                                        @(item.IsActive ? "Active" : "Inactive")
                                    </span>
                                </td>
                                <td data-label="Actions">
                                    <div class="btn-group">
                                        <button class="btn btn-sm btn-outline-info" @onclick="() => ViewItem(item.Id)"
                                            title="View" aria-label="View item">
                                            <i class="fas fa-eye" aria-hidden="true"></i>
                                        </button>
                                        <button class="btn btn-sm btn-outline-primary" @onclick="() => EditItem(item.Id)"
                                            title="Edit" aria-label="Edit item">
                                            <i class="fas fa-edit" aria-hidden="true"></i>
                                        </button>
                                        <button class="btn btn-sm btn-outline-danger" @onclick="() => DeleteItem(item.Id)"
                                            title="Delete" aria-label="Delete item">
                                            <i class="fas fa-trash" aria-hidden="true"></i>
                                        </button>
                                    </div>
                                </td>
                            </tr>
                        }
                    </tbody>
                </table>
            }
        </div>
    </div>
</div>

@code {
    [Parameter] public string? FilterBy { get; set; }
}
```

### **Template: YourList.razor.css**

```css
/* List Page Styling */

.list-page {
    padding: 2rem;
    background-color: #f8f9fa;
    min-height: calc(100vh - 60px);
}

.page-header {
    display: flex;
    justify-content: space-between;
    align-items: flex-start;
    margin-bottom: 2rem;
    gap: 1rem;
}

.header-left {
    flex: 1;
}

.header-right {
    flex-shrink: 0;
}

.page-header h1 {
    font-size: 1.5rem;
    font-weight: 700;
    color: #1a237e;
    margin: 0;
    letter-spacing: -0.5px;
}

.breadcrumb {
    background: transparent;
    padding: 0;
    margin-top: 0.5rem;
    gap: 0.5rem;
}

.breadcrumb-item {
    color: #6c757d;
    font-size: 0.875rem;
}

.breadcrumb-item a {
    color: #0066cc;
    text-decoration: none;
    transition: color 0.2s ease;
}

.breadcrumb-item a:hover {
    color: #0052a3;
    text-decoration: underline;
}

.breadcrumb-item.active {
    color: #1a237e;
    font-weight: 600;
}

/* Content Container */
.content-container {
    display: flex;
    flex-direction: column;
    gap: 1.5rem;
}

/* Stats Row */
.stats-row {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
    gap: 1.5rem;
}

.stat-card {
    background: white;
    padding: 1.5rem;
    border-radius: 12px;
    display: flex;
    align-items: center;
    gap: 1rem;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.08);
    border: 1px solid #e8eaf6;
    transition: all 0.3s ease;
}

.stat-card:hover {
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.12);
    transform: translateY(-2px);
}

.stat-icon {
    width: 48px;
    height: 48px;
    min-width: 48px;
    border-radius: 50%;
    background: #e8eaf6;
    color: #3f51b5;
    display: flex;
    align-items: center;
    justify-content: center;
    font-size: 1.25rem;
}

.stat-info {
    display: flex;
    flex-direction: column;
    gap: 0.25rem;
}

.stat-label {
    font-size: 0.875rem;
    color: #6c757d;
    font-weight: 500;
}

.stat-value {
    font-size: 1.75rem;
    font-weight: 700;
    color: #212121;
}

/* Filter Card */
.filter-card {
    background: white;
    padding: 1.5rem;
    border-radius: 12px;
    display: flex;
    gap: 1rem;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.08);
    border: 1px solid #e8eaf6;
}

.search-box {
    position: relative;
    flex: 1;
}

.search-box i {
    position: absolute;
    left: 1rem;
    top: 50%;
    transform: translateY(-50%);
    color: #adb5bd;
    pointer-events: none;
}

.search-box input {
    width: 100%;
    padding: 0.625rem 1rem 0.625rem 2.5rem;
    border: 1px solid #dee2e6;
    border-radius: 8px;
    font-size: 0.95rem;
    transition: all 0.2s ease;
}

.search-box input:focus {
    outline: none;
    border-color: #0066cc;
    box-shadow: 0 0 0 3px rgba(0, 102, 204, 0.1);
}

/* Table Container */
.table-container {
    background: white;
    border-radius: 12px;
    overflow: hidden;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.08);
    border: 1px solid #e8eaf6;
}

.custom-table {
    width: 100%;
    border-collapse: collapse;
    margin: 0;
}

.custom-table thead {
    background: linear-gradient(135deg, #f8f9fa 0%, #ffffff 100%);
    border-bottom: 2px solid #e8eaf6;
}

.custom-table th {
    padding: 1.25rem;
    text-align: left;
    font-weight: 600;
    color: #1a237e;
    font-size: 0.95rem;
    letter-spacing: 0.3px;
    text-transform: uppercase;
}

.custom-table tbody tr {
    border-bottom: 1px solid #e8eaf6;
    transition: background-color 0.2s ease;
}

.custom-table tbody tr:hover {
    background-color: #f8f9fa;
}

.custom-table tbody tr:last-child {
    border-bottom: none;
}

.custom-table td {
    padding: 1.25rem;
    color: #212121;
    font-size: 0.95rem;
}

/* Status Badge */
.status-badge {
    display: inline-block;
    padding: 0.375rem 0.75rem;
    border-radius: 20px;
    font-size: 0.85rem;
    font-weight: 600;
    text-transform: capitalize;
}

.status-badge.active {
    background-color: #e8f5e9;
    color: #2e7d32;
}

.status-badge.inactive {
    background-color: #ffebee;
    color: #c62828;
}

/* Button Group */
.btn-group {
    display: flex;
    gap: 0.5rem;
}

.btn {
    padding: 0.5rem 1rem;
    border: 1px solid #dee2e6;
    border-radius: 6px;
    cursor: pointer;
    font-size: 0.875rem;
    font-weight: 500;
    transition: all 0.2s ease;
    display: inline-flex;
    align-items: center;
    gap: 0.5rem;
}

.btn-primary {
    background-color: #0066cc;
    color: white;
    border-color: #0066cc;
}

.btn-primary:hover {
    background-color: #0052a3;
    border-color: #0052a3;
    box-shadow: 0 2px 8px rgba(0, 102, 204, 0.2);
}

.btn-outline-info {
    background-color: transparent;
    color: #06b6d4;
    border-color: #06b6d4;
}

.btn-outline-info:hover {
    background-color: #ecf9fc;
    color: #0891b2;
    border-color: #0891b2;
}

.btn-outline-primary {
    background-color: transparent;
    color: #0066cc;
    border-color: #0066cc;
}

.btn-outline-primary:hover {
    background-color: #f0f4ff;
    color: #0052a3;
    border-color: #0052a3;
}

.btn-outline-danger {
    background-color: transparent;
    color: #dc2626;
    border-color: #dc2626;
}

.btn-outline-danger:hover {
    background-color: #fee2e2;
    color: #b91c1c;
    border-color: #b91c1c;
}

.btn-sm {
    padding: 0.375rem 0.75rem;
    font-size: 0.8rem;
}

/* Utility Classes */
.text-center {
    text-align: center;
}

.py-5 {
    padding-top: 3rem;
    padding-bottom: 3rem;
}

.mt-2 {
    margin-top: 0.5rem;
}

.spinner-border {
    display: inline-block;
    width: 2rem;
    height: 2rem;
    border: 0.25em solid currentColor;
    border-right-color: transparent;
    border-radius: 50%;
    animation: spinner-rotate 0.75s linear infinite;
}

.text-primary {
    color: #0066cc;
}

.text-muted {
    color: #6c757d;
}

@keyframes spinner-rotate {
    to {
        transform: rotate(360deg);
    }
}

/* Responsive Design */

@media (max-width: 1200px) {
    .list-page {
        padding: 1.5rem;
    }

    .page-header {
        flex-direction: column;
        align-items: flex-start;
    }

    .page-header h1 {
        font-size: 1.25rem;
    }

    .stats-row {
        grid-template-columns: 1fr;
    }

    .filter-card {
        flex-direction: column;
    }

    .custom-table th,
    .custom-table td {
        padding: 1rem;
        font-size: 0.9rem;
    }
}

@media (max-width: 768px) {
    .list-page {
        padding: 1rem;
        min-height: calc(100vh - 56px);
    }

    .page-header h1 {
        font-size: 1.125rem;
    }

    .breadcrumb {
        display: none;
    }

    .stats-row {
        gap: 1rem;
    }

    .stat-card {
        padding: 1rem;
        gap: 0.75rem;
    }

    .stat-icon {
        width: 40px;
        height: 40px;
        font-size: 1.1rem;
    }

    .stat-value {
        font-size: 1.5rem;
    }

    .filter-card {
        padding: 1rem;
        gap: 0.75rem;
    }

    .search-box input {
        font-size: 16px;
    }

    .custom-table {
        font-size: 0.85rem;
    }

    .custom-table th,
    .custom-table td {
        padding: 0.75rem;
    }

    .custom-table th {
        font-size: 0.8rem;
    }

    .btn-group {
        gap: 0.375rem;
    }

    .btn-sm {
        padding: 0.3rem 0.6rem;
        font-size: 0.75rem;
    }
}

@media (max-width: 576px) {
    .list-page {
        padding: 0.75rem;
    }

    .page-header {
        gap: 0.5rem;
    }

    .page-header h1 {
        font-size: 1rem;
    }

    .header-right .btn {
        width: 100%;
        justify-content: center;
    }

    .content-container {
        gap: 1rem;
    }

    .stats-row {
        gap: 0.75rem;
        grid-template-columns: 1fr;
    }

    .stat-card {
        padding: 0.75rem;
    }

    .stat-label {
        font-size: 0.75rem;
    }

    .stat-value {
        font-size: 1.25rem;
    }

    .filter-card {
        flex-direction: column;
        align-items: stretch;
        padding: 0.75rem;
        gap: 0.5rem;
    }

    .search-box {
        width: 100%;
    }

    .custom-table {
        font-size: 0.8rem;
    }

    .custom-table th,
    .custom-table td {
        padding: 0.5rem;
    }

    .custom-table thead {
        display: none;
    }

    .custom-table tbody tr {
        display: grid;
        grid-template-columns: 1fr;
        gap: 0.5rem;
        padding: 1rem 0.5rem;
        margin-bottom: 1rem;
        background: white;
        border: 1px solid #e8eaf6;
        border-radius: 8px;
    }

    .custom-table tbody tr:last-child {
        border: 1px solid #e8eaf6;
    }

    .custom-table td {
        display: grid;
        grid-template-columns: 120px 1fr;
        gap: 0.5rem;
        padding: 0.5rem;
        align-items: center;
    }

    .custom-table td::before {
        content: attr(data-label);
        font-weight: 600;
        color: #1a237e;
        font-size: 0.75rem;
        text-transform: uppercase;
    }

    .btn-group {
        display: flex;
        gap: 0.25rem;
        width: 100%;
    }

    .btn-group .btn {
        flex: 1;
        padding: 0.375rem;
        font-size: 0.7rem;
    }

    .btn-group .btn i {
        margin-right: 0;
    }

    .btn-group .btn span {
        display: none;
    }
}

@media (max-width: 360px) {
    .list-page {
        padding: 0.5rem;
    }

    .page-header h1 {
        font-size: 0.95rem;
    }

    .stat-card {
        flex-direction: column;
        align-items: center;
        text-align: center;
        padding: 0.75rem;
    }

    .stat-info {
        width: 100%;
    }

    .custom-table tbody tr {
        margin-bottom: 0.75rem;
        padding: 0.75rem 0.375rem;
    }

    .custom-table td {
        grid-template-columns: 1fr;
        gap: 0.25rem;
    }

    .custom-table td::before {
        font-size: 0.7rem;
        margin-bottom: 0.25rem;
    }
}
```

---

## 2?? FORM/CREATE-EDIT PAGE TEMPLATE

Use this for: ClientCreateEdit, EmployeeCreateEdit, etc.

### **Template: YourCreateEdit.razor.cs**

```csharp
using Microsoft.AspNetCore.Components;
using QMCH.Interfaces.Services;

namespace QMCH.Components.Pages
{
    public partial class YourCreateEdit
    {
        [Inject] protected IYourService? Service { get; set; }
        [Inject] protected NavigationManager? Navigation { get; set; }

        [Parameter] public int? Id { get; set; }

        private YourItem? item = new();
        private bool isSaving = false;
        private bool isLoading = true;
        private bool hasError = false;
        private string errorMessage = "";
        private string pageTitle = "Create";

        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (Id.HasValue)
                {
                    pageTitle = "Edit";
                    item = await Service?.GetByIdAsync(Id.Value)!;
                    if (item == null)
                    {
                        hasError = true;
                        errorMessage = "Item not found";
                    }
                }
                isLoading = false;
            }
            catch (Exception ex)
            {
                hasError = true;
                errorMessage = ex.Message;
                isLoading = false;
            }
        }

        private async Task SaveItem()
        {
            try
            {
                isSaving = true;
                hasError = false;

                if (item == null)
                    throw new InvalidOperationException("Item is null");

                if (item.Id == 0)
                    await Service?.CreateAsync(item)!;
                else
                    await Service?.UpdateAsync(item)!;

                Navigation?.NavigateTo("/your-path/list");
            }
            catch (Exception ex)
            {
                hasError = true;
                errorMessage = ex.Message;
                isSaving = false;
            }
        }

        private void Cancel() => Navigation?.NavigateTo("/your-path/list");
    }
}
```

### **Template: YourCreateEdit.razor**

```razor
@page "/your-path/create"
@page "/your-path/edit/{id:int}"
@inject IYourService Service
@inject NavigationManager Navigation
@rendermode InteractiveServer

<PageTitle>@pageTitle Item - QMCH</PageTitle>

<div class="form-page">
    @if (hasError)
    {
        <div class="alert alert-danger alert-dismissible fade show" role="alert">
            <strong>Error:</strong> @errorMessage
            <button type="button" class="btn-close" @onclick="@(() => hasError = false)" aria-label="Close"></button>
        </div>
    }

    @if (isLoading)
    {
        <div class="text-center py-5">
            <div class="spinner-border text-primary" role="status">
                <span class="visually-hidden">Loading...</span>
            </div>
            <p class="mt-2">Loading...</p>
        </div>
    }
    else
    {
        <div class="form-header">
            <h1>@pageTitle Item</h1>
        </div>

        <EditForm Model="item" OnValidSubmit="SaveItem" class="form-container">
            <DataAnnotationsValidator />
            <ValidationSummary />

            <div class="form-section">
                <div class="form-group">
                    <label for="name">Name:</label>
                    <InputText id="name" class="form-control" @bind-Value="item!.Name" placeholder="Enter name" required />
                    <ValidationMessage For="@(() => item!.Name)" />
                </div>

                <div class="form-group">
                    <label for="description">Description:</label>
                    <InputTextArea id="description" class="form-control" @bind-Value="item!.Description" placeholder="Enter description" />
                    <ValidationMessage For="@(() => item!.Description)" />
                </div>

                <div class="form-group">
                    <label>
                        <InputCheckbox @bind-Value="item!.IsActive" />
                        Active
                    </label>
                </div>
            </div>

            <div class="form-actions">
                <button type="submit" class="btn btn-primary" disabled="@isSaving">
                    @(isSaving ? "Saving..." : "Save")
                </button>
                <button type="button" class="btn btn-secondary" @onclick="Cancel" disabled="@isSaving">
                    Cancel
                </button>
            </div>
        </EditForm>
    }
</div>

@code {
    [Parameter] public int? Id { get; set; }
}
```

### **Template: YourCreateEdit.razor.css**

```css
.form-page {
    padding: 2rem;
    background-color: #f8f9fa;
    min-height: calc(100vh - 60px);
}

.form-header {
    margin-bottom: 2rem;
}

.form-header h1 {
    font-size: 1.5rem;
    font-weight: 700;
    color: #1a237e;
    margin: 0;
}

.form-container {
    max-width: 800px;
    margin: 0 auto;
    background: white;
    padding: 2rem;
    border-radius: 12px;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.08);
    border: 1px solid #e8eaf6;
}

.form-section {
    margin-bottom: 2rem;
}

.form-group {
    margin-bottom: 1.5rem;
}

.form-group label {
    display: block;
    margin-bottom: 0.5rem;
    font-weight: 500;
    color: #1a237e;
    font-size: 0.95rem;
}

.form-control {
    width: 100%;
    padding: 0.625rem 1rem;
    border: 1px solid #dee2e6;
    border-radius: 8px;
    font-size: 0.95rem;
    transition: all 0.2s ease;
    font-family: inherit;
}

.form-control:focus {
    outline: none;
    border-color: #0066cc;
    box-shadow: 0 0 0 3px rgba(0, 102, 204, 0.1);
}

.form-actions {
    display: flex;
    gap: 1rem;
    margin-top: 2rem;
    padding-top: 2rem;
    border-top: 1px solid #e8eaf6;
}

.btn {
    padding: 0.75rem 1.5rem;
    border: none;
    border-radius: 6px;
    cursor: pointer;
    font-weight: 500;
    transition: all 0.2s ease;
    font-size: 0.95rem;
}

.btn-primary {
    background-color: #0066cc;
    color: white;
}

.btn-primary:hover:not(:disabled) {
    background-color: #0052a3;
    box-shadow: 0 2px 8px rgba(0, 102, 204, 0.2);
}

.btn-secondary {
    background-color: #6c757d;
    color: white;
}

.btn-secondary:hover:not(:disabled) {
    background-color: #5a6268;
}

.btn:disabled {
    opacity: 0.6;
    cursor: not-allowed;
}

.validation-message {
    color: #dc2626;
    font-size: 0.875rem;
    margin-top: 0.25rem;
}

/* Responsive */
@media (max-width: 768px) {
    .form-page {
        padding: 1rem;
    }

    .form-container {
        padding: 1.5rem;
    }

    .form-header h1 {
        font-size: 1.25rem;
    }

    .form-actions {
        flex-direction: column;
    }

    .btn {
        width: 100%;
    }

    .form-control {
        font-size: 16px;
    }
}

@media (max-width: 576px) {
    .form-page {
        padding: 0.75rem;
    }

    .form-container {
        padding: 1rem;
        margin: 0;
    }

    .form-header h1 {
        font-size: 1.125rem;
    }

    .form-group {
        margin-bottom: 1rem;
    }

    .form-actions {
        gap: 0.5rem;
    }
}

@media (max-width: 360px) {
    .form-page {
        padding: 0.5rem;
    }

    .form-container {
        padding: 0.75rem;
    }

    .form-header h1 {
        font-size: 1rem;
    }
}
```

---

## ? QUICK ADAPTATION GUIDE

### **For EmployeeList.razor, change:**
```
YourList ? EmployeeList
YourItem ? Employee
YourService ? IEmployeeService
your-path ? employee
YOUR ITEMS ? EMPLOYEES
List<YourItem> ? List<Employee>
```

### **For ClientCreateEdit.razor, change:**
```
YourCreateEdit ? ClientCreateEdit
YourItem ? Client
YourService ? IClientService
your-path ? client
Client Item ? Client
List<YourItem> ? List<Client>
```

---

## ?? SPEED UP TIPS

1. **Find & Replace in IDE:**
   - Use Ctrl+H (Find & Replace)
   - Replace "YourList" ? "EmployeeList"
   - Replace "your-path" ? "/employee/list"
   - Etc.

2. **Copy entire template section**
   - Don't type character by character
   - Use Ctrl+C / Ctrl+V

3. **Test as you go**
   - Save files
   - Run dotnet build
   - Check for errors immediately
   - Fix errors before moving to next file

4. **Use git diff**
   - Verify changes before committing
   - Compare with PatientList.razor
   - Ensure consistency

---

**This kit will cut your refactoring time in HALF! ??**

