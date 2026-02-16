# BLAZOR PAGE IMPLEMENTATION EXAMPLES

## Template 1: Simple List Page

```razor
@page "/hmc/examples/list"
@inject IExampleService ExampleService
@inject NavigationManager Navigation
@rendermode InteractiveServer

<PageTitle>Examples - QMCH</PageTitle>

<div class="container-fluid py-4">
    <!-- Header -->
    <div class="page-header mb-4">
        <div class="row align-items-center">
            <div class="col">
                <h1 class="h3 mb-0">Examples</h1>
                <p class="text-muted">Manage examples</p>
            </div>
            <div class="col-auto">
                <button class="btn btn-primary" @onclick="() => Navigate('/hmc/examples/form')">
                    <i class="bi bi-plus-lg"></i> Add Example
                </button>
            </div>
        </div>
    </div>

    <!-- Search -->
    <div class="card mb-4">
        <div class="card-body">
            <div class="row g-3">
                <div class="col-md-6">
                    <input type="text" class="form-control" placeholder="Search..."
                           @bind="SearchTerm" @oninput="@((e) => OnSearch(e.Value?.ToString() ?? ""))" />
                </div>
                <div class="col-md-2">
                    <button class="btn btn-outline-secondary w-100" @onclick="ResetSearch">Reset</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Table -->
    @if (IsLoading)
    {
        <div class="text-center py-5">
            <Loading IsVisible="true" LoadingMessage="Loading..." />
        </div>
    }
    else if (FilteredItems.Count > 0)
    {
        <div class="card">
            <div class="table-responsive">
                <table class="table table-striped table-hover mb-0">
                    <thead class="table-light">
                        <tr>
                            <th>ID</th>
                            <th>Name</th>
                            <th>Status</th>
                            <th>Created</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        @foreach (var item in PaginatedItems)
                        {
                            <tr>
                                <td>@item.Id</td>
                                <td>@item.Name</td>
                                <td><span class="badge bg-success">@item.Status</span></td>
                                <td>@item.CreatedAt.ToString("MMM dd, yyyy")</td>
                                <td>
                                    <div class="btn-group btn-group-sm">
                                        <button class="btn btn-info" @onclick="() => View(item.Id)">
                                            <i class="bi bi-eye"></i>
                                        </button>
                                        <button class="btn btn-primary" @onclick="() => Edit(item.Id)">
                                            <i class="bi bi-pencil"></i>
                                        </button>
                                        <button class="btn btn-danger" @onclick="() => Delete(item.Id)">
                                            <i class="bi bi-trash"></i>
                                        </button>
                                    </div>
                                </td>
                            </tr>
                        }
                    </tbody>
                </table>
            </div>
            <div class="card-footer">
                <Pagination CurrentPage="CurrentPage" PageSize="PageSize" 
                           TotalItems="FilteredItems.Count"
                           OnPageChanged="@((p) => CurrentPage = p)" />
            </div>
        </div>
    }
    else
    {
        <Alert Type="info" Title="No Data" Message="No examples found" />
    }
</div>

@code {
    private List<Example> AllItems = new();
    private List<Example> FilteredItems = new();
    private List<Example> PaginatedItems = new();

    private string SearchTerm = "";
    private int CurrentPage = 1;
    private int PageSize = 10;
    private bool IsLoading = true;

    protected override async Task OnInitializedAsync()
    {
        await LoadData();
    }

    private async Task LoadData()
    {
        IsLoading = true;
        try
        {
            AllItems = await ExampleService.GetAllAsync();
            ApplyFilters();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    private void ApplyFilters()
    {
        FilteredItems = AllItems.Where(x =>
            string.IsNullOrEmpty(SearchTerm) ||
            x.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)
        ).ToList();

        CurrentPage = 1;
        UpdatePagination();
    }

    private void UpdatePagination()
    {
        PaginatedItems = FilteredItems
            .Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize)
            .ToList();
    }

    private void OnSearch(string term)
    {
        SearchTerm = term;
        ApplyFilters();
    }

    private void ResetSearch()
    {
        SearchTerm = "";
        ApplyFilters();
    }

    private void View(int id) => Navigation.NavigateTo($"/hmc/examples/detail/{id}");
    private void Edit(int id) => Navigation.NavigateTo($"/hmc/examples/form/{id}");

    private void Delete(int id)
    {
        if (confirm("Are you sure?"))
        {
            // Implement delete
        }
    }

    private void Navigate(string url) => Navigation.NavigateTo(url);
}
```

---

## Template 2: Form Page (Create/Edit)

```razor
@page "/hmc/examples/form"
@page "/hmc/examples/form/{exampleId:int}"
@inject IExampleService ExampleService
@inject NavigationManager Navigation
@rendermode InteractiveServer

<PageTitle>@(ExampleId > 0 ? "Edit" : "Add") Example - QMCH</PageTitle>

<div class="container-fluid py-4">
    <!-- Header -->
    <div class="page-header mb-4">
        <h1 class="h3 mb-0">@(ExampleId > 0 ? "Edit Example" : "Add New Example")</h1>
    </div>

    <!-- Alert -->
    @if (ShowAlert)
    {
        <Alert Type="@AlertType" Title="@AlertTitle" Message="@AlertMessage" 
               OnClose="@(() => ShowAlert = false)" />
    }

    @if (IsLoading)
    {
        <Loading IsVisible="true" />
    }
    else
    {
        <EditForm Model="Model" OnValidSubmit="HandleSubmit">
            <DataAnnotationsValidator />
            <ValidationSummary />

            <div class="row">
                <div class="col-lg-8">
                    <!-- Main Form -->
                    <div class="card mb-4">
                        <div class="card-header bg-light">
                            <h5 class="mb-0">Details</h5>
                        </div>
                        <div class="card-body">
                            <FormInput Label="Name" Type="text" @bind-Value="Model.Name" Required="true" />
                            <FormInput Label="Description" Type="textarea" @bind-Value="Model.Description" />
                            <FormInput Label="Status" Type="select" @bind-Value="Model.Status"
                                      Options="StatusOptions" />
                        </div>
                    </div>
                </div>

                <!-- Sidebar -->
                <div class="col-lg-4">
                    <div class="card position-sticky" style="top: 20px;">
                        <div class="card-body">
                            <div class="d-grid gap-2">
                                <button type="submit" class="btn btn-primary btn-lg">
                                    <i class="bi bi-check-lg"></i>
                                    @(ExampleId > 0 ? "Update" : "Create")
                                </button>
                                <button type="button" class="btn btn-outline-secondary" 
                                       @onclick="() => Navigation.NavigateTo('/hmc/examples/list')">
                                    Cancel
                                </button>
                            </div>
                            
                            @if (ExampleId > 0)
                            {
                                <hr />
                                <small class="text-muted d-block">
                                    <strong>Created:</strong> @Model.CreatedAt.ToString("MMM dd, yyyy HH:mm")
                                    <br />
                                    <strong>Updated:</strong> @Model.UpdatedAt.ToString("MMM dd, yyyy HH:mm")
                                </small>
                            }
                        </div>
                    </div>
                </div>
            </div>
        </EditForm>
    }
</div>

@code {
    [Parameter]
    public int ExampleId { get; set; }

    private Example Model = new();
    private bool IsLoading = false;
    private bool ShowAlert = false;
    private string AlertType = "success";
    private string AlertTitle = "";
    private string AlertMessage = "";

    private Dictionary<string, string> StatusOptions = new()
    {
        { "Active", "Active" },
        { "Inactive", "Inactive" }
    };

    protected override async Task OnInitializedAsync()
    {
        if (ExampleId > 0)
        {
            IsLoading = true;
            try
            {
                Model = await ExampleService.GetByIdAsync(ExampleId) ?? new Example();
            }
            catch (Exception ex)
            {
                ShowAlert = true;
                AlertType = "danger";
                AlertTitle = "Error";
                AlertMessage = $"Failed to load: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }
        else
        {
            Model = new Example { Status = "Active" };
        }
    }

    private async Task HandleSubmit()
    {
        try
        {
            if (ExampleId > 0)
            {
                await ExampleService.UpdateAsync(Model);
                ShowAlert = true;
                AlertType = "success";
                AlertTitle = "Success";
                AlertMessage = "Updated successfully!";
            }
            else
            {
                await ExampleService.CreateAsync(Model);
                ShowAlert = true;
                AlertType = "success";
                AlertTitle = "Success";
                AlertMessage = "Created successfully!";
                await Task.Delay(1500);
                Navigation.NavigateTo("/hmc/examples/list");
            }
        }
        catch (Exception ex)
        {
            ShowAlert = true;
            AlertType = "danger";
            AlertTitle = "Error";
            AlertMessage = $"Failed to save: {ex.Message}";
        }
    }
}
```

---

## Template 3: Detail/View Page

```razor
@page "/hmc/examples/detail/{exampleId:int}"
@inject IExampleService ExampleService
@inject NavigationManager Navigation
@rendermode InteractiveServer

<PageTitle>Example Details - QMCH</PageTitle>

<div class="container-fluid py-4">
    <!-- Header with Actions -->
    <div class="page-header mb-4">
        <div class="row align-items-center">
            <div class="col">
                <h1 class="h3 mb-0">@Model?.Name</h1>
            </div>
            <div class="col-auto">
                <div class="btn-group">
                    <button class="btn btn-primary" @onclick="() => Navigation.NavigateTo($'/hmc/examples/form/{ExampleId}')">
                        <i class="bi bi-pencil"></i> Edit
                    </button>
                    <button class="btn btn-danger" @onclick="DeleteExample">
                        <i class="bi bi-trash"></i> Delete
                    </button>
                </div>
            </div>
        </div>
    </div>

    @if (IsLoading)
    {
        <Loading IsVisible="true" />
    }
    else if (Model != null)
    {
        <!-- Content -->
        <div class="row">
            <div class="col-lg-8">
                <div class="card mb-4">
                    <div class="card-header bg-light">
                        <h5 class="mb-0">Information</h5>
                    </div>
                    <div class="card-body">
                        <dl class="row">
                            <dt class="col-sm-3">Name:</dt>
                            <dd class="col-sm-9">@Model.Name</dd>

                            <dt class="col-sm-3">Status:</dt>
                            <dd class="col-sm-9">
                                <span class="badge bg-success">@Model.Status</span>
                            </dd>

                            <dt class="col-sm-3">Description:</dt>
                            <dd class="col-sm-9">@Model.Description</dd>

                            <dt class="col-sm-3">Created:</dt>
                            <dd class="col-sm-9">@Model.CreatedAt.ToString("MMM dd, yyyy HH:mm")</dd>

                            <dt class="col-sm-3">Updated:</dt>
                            <dd class="col-sm-9">@Model.UpdatedAt.ToString("MMM dd, yyyy HH:mm")</dd>
                        </dl>
                    </div>
                </div>
            </div>

            <!-- Sidebar -->
            <div class="col-lg-4">
                <div class="card">
                    <div class="card-header bg-light">
                        <h5 class="mb-0">Quick Actions</h5>
                    </div>
                    <div class="card-body d-grid gap-2">
                        <a href="" @onclick:preventDefault @onclick="() => Navigation.NavigateTo($'/hmc/examples/form/{ExampleId}')" 
                           class="btn btn-primary">
                            <i class="bi bi-pencil"></i> Edit Example
                        </a>
                        <button class="btn btn-outline-danger" @onclick="DeleteExample">
                            <i class="bi bi-trash"></i> Delete Example
                        </button>
                    </div>
                </div>
            </div>
        </div>
    }
    else
    {
        <Alert Type="danger" Title="Error" Message="Example not found" />
    }
</div>

@code {
    [Parameter]
    public int ExampleId { get; set; }

    private Example? Model;
    private bool IsLoading = true;

    protected override async Task OnInitializedAsync()
    {
        await LoadData();
    }

    private async Task LoadData()
    {
        IsLoading = true;
        try
        {
            Model = await ExampleService.GetByIdAsync(ExampleId);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
        }
    }

    private async Task DeleteExample()
    {
        if (confirm("Are you sure you want to delete this?"))
        {
            try
            {
                await ExampleService.DeleteAsync(ExampleId);
                Navigation.NavigateTo("/hmc/examples/list");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
```

---

## Template 4: Dashboard/Summary Page

```razor
@page "/dashboard/summary"
@inject IPatientService PatientService
@inject IStaffService StaffService
@inject IVisitService VisitService
@rendermode InteractiveServer

<PageTitle>Dashboard - QMCH</PageTitle>

<div class="container-fluid py-4">
    <!-- Header -->
    <div class="page-header mb-4">
        <h1 class="h3 mb-0">Dashboard</h1>
        <p class="text-muted">System overview and key metrics</p>
    </div>

    @if (IsLoading)
    {
        <Loading IsVisible="true" LoadingMessage="Loading dashboard..." />
    }
    else
    {
        <!-- KPI Cards -->
        <div class="row g-3 mb-4">
            <div class="col-md-3">
                <div class="card border-0 shadow-sm">
                    <div class="card-body">
                        <p class="text-muted small mb-1">Total Patients</p>
                        <h3 class="mb-0">@TotalPatients</h3>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card border-0 shadow-sm">
                    <div class="card-body">
                        <p class="text-muted small mb-1">Active Staff</p>
                        <h3 class="mb-0">@ActiveStaff</h3>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card border-0 shadow-sm">
                    <div class="card-body">
                        <p class="text-muted small mb-1">Today's Visits</p>
                        <h3 class="mb-0">@TodayVisits</h3>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card border-0 shadow-sm">
                    <div class="card-body">
                        <p class="text-muted small mb-1">Pending Tasks</p>
                        <h3 class="mb-0">@PendingTasks</h3>
                    </div>
                </div>
            </div>
        </div>

        <!-- Charts/Tables -->
        <div class="row g-3">
            <div class="col-lg-6">
                <div class="card">
                    <div class="card-header bg-light">
                        <h5 class="mb-0">Recent Activity</h5>
                    </div>
                    <div class="table-responsive">
                        <table class="table table-sm mb-0">
                            <thead>
                                <tr>
                                    <th>Item</th>
                                    <th>Date</th>
                                    <th>Status</th>
                                </tr>
                            </thead>
                            <tbody>
                                @foreach (var item in RecentItems.Take(5))
                                {
                                    <tr>
                                        <td>@item.Name</td>
                                        <td>@item.Date.ToString("MMM dd")</td>
                                        <td><span class="badge">@item.Status</span></td>
                                    </tr>
                                }
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>

            <div class="col-lg-6">
                <div class="card">
                    <div class="card-header bg-light">
                        <h5 class="mb-0">Quick Stats</h5>
                    </div>
                    <div class="card-body">
                        <div class="mb-3">
                            <label class="form-label">System Health</label>
                            <div class="progress">
                                <div class="progress-bar bg-success" style="width: 95%">95%</div>
                            </div>
                        </div>
                        <div class="mb-3">
                            <label class="form-label">Database Usage</label>
                            <div class="progress">
                                <div class="progress-bar bg-info" style="width: 45%">45%</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    }
</div>

@code {
    private bool IsLoading = true;
    private int TotalPatients = 0;
    private int ActiveStaff = 0;
    private int TodayVisits = 0;
    private int PendingTasks = 0;
    private List<dynamic> RecentItems = new();

    protected override async Task OnInitializedAsync()
    {
        await LoadDashboard();
    }

    private async Task LoadDashboard()
    {
        IsLoading = true;
        try
        {
            var patients = await PatientService.GetAllAsync();
            var staff = await StaffService.GetAllAsync();
            var visits = await VisitService.GetAllAsync();

            TotalPatients = patients.Count;
            ActiveStaff = staff.Count(s => s.Status == "Active");
            TodayVisits = visits.Count(v => v.ScheduledStartTime.Date == DateTime.Now.Date);
            PendingTasks = 5; // Calculate as needed
        }
        finally
        {
            IsLoading = false;
        }
    }
}
```

---

## Tips for Using These Templates

1. **Copy and adapt** - Don't copy directly; use as reference
2. **Replace "Example"** - With your actual entity name
3. **Update service names** - Use correct service injections
4. **Adjust fields** - Add/remove form fields as needed
5. **Customize styling** - Match your brand colors
6. **Add validation** - Use DataAnnotations on models
7. **Handle errors** - Always show user-friendly messages
8. **Test thoroughly** - Before deploying to production

---

**Ready to create stunning Blazor pages!**
