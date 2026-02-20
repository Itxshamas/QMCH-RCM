using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using QMCH.Interfaces.Services;
using QMCH.Models;
using QMCH.Services;

namespace QMCH.Components.Pages
{
    public partial class Payroll : ComponentBase
    {
        [Inject] public ITimesheetService TimesheetService { get; set; }
        [Inject] public IEmployeeService EmployeeService { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        [Inject] public IJSRuntime JSRuntime { get; set; }

        protected List<TimesheetPayroll> allPayrolls = new();
        protected List<TimesheetPayroll> filteredPayrolls = new();
        protected List<Employee> nurses = new();
        protected List<string> categories = new() { "Nurse", "Doctor", "Assistant" };

        protected bool isLoading = true;
        protected bool hasError = false;
        protected string errorMessage = string.Empty;

        protected string selectedCategory = string.Empty;
        protected int selectedNurseId = 0;
        protected DateTime? fromDate = null;
        protected DateTime? toDate = null;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        protected async Task LoadData()
        {
            isLoading = true;
            try
            {
                allPayrolls = await TimesheetService.GetPayrollsAsync();
                nurses = await EmployeeService.GetAllAsync();
                filteredPayrolls = new List<TimesheetPayroll>(allPayrolls);
            }
            catch (Exception ex)
            {
                hasError = true;
                errorMessage = ex.Message;
            }
            finally
            {
                isLoading = false;
            }
        }

        protected void ApplyFilters()
        {
            filteredPayrolls = allPayrolls
                .Where(p =>
                    (string.IsNullOrEmpty(selectedCategory) || p.Category == selectedCategory) &&
                    (selectedNurseId == 0 || p.EmployeeId == selectedNurseId) &&
                    (!fromDate.HasValue || p.PeriodStart >= fromDate.Value) &&
                    (!toDate.HasValue || p.PeriodEnd <= toDate.Value))
                .ToList();
        }

        protected void ClearFilters()
        {
            selectedCategory = string.Empty;
            selectedNurseId = 0;
            fromDate = null;
            toDate = null;
            filteredPayrolls = new List<TimesheetPayroll>(allPayrolls);
        }

        protected void CreateNew()
        {
            Navigation.NavigateTo("/timesheet/payroll/list/create");
        }

        protected void EditPayroll(int id)
        {
            Navigation.NavigateTo($"/timesheet/payroll/list/create?id={id}");
        }

        protected async Task DeletePayroll(int id)
        {
            if (await JSRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to delete this timesheet?"))
            {
                try
                {
                    await TimesheetService.DeletePayrollAsync(id);
                    await LoadData();
                }
                catch (Exception ex)
                {
                    hasError = true;
                    errorMessage = ex.Message;
                }
            }
        }

        protected string GetStatusBadge(string status) => status switch
        {
            "Approved" => "badge-success",
            "Pending" => "badge-warning",
            "Rejected" => "badge-danger",
            "Paid" => "badge-primary",
            _ => "badge-secondary"
        };
    }
}
