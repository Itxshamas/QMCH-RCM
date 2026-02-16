using QMCH.Models;
using QMCH.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace QMCH.Components.Pages
{
    public partial class AttendanceList : ComponentBase
    {
        [Inject]
        private IEmployeeService EmployeeService { get; set; } = null!;

        private List<Attendance> items = new();
        private List<Employee> employees = new();
        private bool isLoading = true;

        private bool showModal = false;
        private bool showNewModal = false;
        private bool showSearchModal = false;

        private Attendance newItem = new() { Date = DateTime.Today, Status = "Present" };
        private string clockInStr = "09:00";
        private bool isSaving = false;

        // Filters
        private string filterBusinessUnit = string.Empty;
        private string filterDepartment = string.Empty;
        private string filterJobTitle = string.Empty;
        private DateTime? searchFromDate = DateTime.Today.AddDays(-7);
        private DateTime? searchToDate = DateTime.Today;
        private int? searchEmployeeId;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            isLoading = true;
            try
            {
                items = await EmployeeService.GetAttendanceAsync();
                employees = await EmployeeService.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading attendance: {ex.Message}");
            }
            finally
            {
                isLoading = false;
            }
        }

        private async Task Save()
        {
            isSaving = true;
            try
            {
                if (TimeSpan.TryParse(clockInStr, out var ci)) newItem.ClockIn = ci;
                // determine status
                if (string.IsNullOrEmpty(newItem.Status)) newItem.Status = "Present";

                if (newItem.EmployeeId.HasValue)
                {
                    var emp = employees.FirstOrDefault(e => e.Id == newItem.EmployeeId.Value);
                    if (emp != null) newItem.EmployeeName = emp.FirstName + " " + emp.LastName;
                }

                await EmployeeService.CreateAttendanceAsync(newItem);
                showNewModal = false;
                await LoadData();
                newItem = new() { Date = DateTime.Today, Status = "Present" };
                clockInStr = "09:00";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving attendance: {ex.Message}");
            }
            finally
            {
                isSaving = false;
            }
        }

        private async Task ApplySearchFromModal()
        {
            // Basic filter application
            isLoading = true;
            try
            {
                var all = await EmployeeService.GetAttendanceAsync();
                var q = all.AsQueryable();
                if (searchFromDate.HasValue) q = q.Where(a => a.Date >= searchFromDate.Value);
                if (searchToDate.HasValue) q = q.Where(a => a.Date <= searchToDate.Value);
                if (searchEmployeeId.HasValue) q = q.Where(a => a.EmployeeId == searchEmployeeId.Value);
                items = q.ToList();
            }
            finally
            {
                isLoading = false;
                showSearchModal = false;
            }
        }

        private void ClearFilters()
        {
            filterBusinessUnit = filterDepartment = filterJobTitle = string.Empty;
            searchFromDate = DateTime.Today.AddDays(-7);
            searchToDate = DateTime.Today;
            searchEmployeeId = null;
            _ = LoadData();
        }
    }
}
