using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using QMCH.Models;
using QMCH.Services;

namespace QMCH.Components.Pages
{
    public partial class PayrollCreateEdit : ComponentBase
    {
        [Inject] public ITimesheetService TimesheetService { get; set; }
        [Inject] public IEmployeeService EmployeeService { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }

        [SupplyParameterFromQuery]
        public int? Id { get; set; }

        protected TimesheetPayroll model = new();
        protected List<Employee> nurses = new();

        protected bool isEdit = false;
        protected bool isLoading = true;
        protected bool hasError = false;
        protected string errorMessage = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                nurses = await EmployeeService.GetAllAsync();

                if (Id.HasValue)
                {
                    var payroll = await TimesheetService.GetPayrollByIdAsync(Id.Value);

                    if (payroll != null)
                    {
                        model = payroll;
                        isEdit = true;
                    }
                    else
                    {
                        hasError = true;
                        errorMessage = "Timesheet not found.";
                    }
                }
                else
                {
                    model = new TimesheetPayroll
                    {
                        PeriodStart = DateTime.Today.AddDays(-14),
                        PeriodEnd = DateTime.Today,
                        Status = "Pending"
                    };
                }
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

        protected async Task Save()
        {
            try
            {
                if (model.EmployeeId <= 0)
                {
                    hasError = true;
                    errorMessage = "Please select an employee.";
                    return;
                }

                var selected = nurses.FirstOrDefault(x => x.Id == model.EmployeeId);
                if (selected != null)
                    model.EmployeeName = $"{selected.FirstName} {selected.LastName}";

                if (model.PeriodStart >= model.PeriodEnd)
                {
                    hasError = true;
                    errorMessage = "End date must be after Start date.";
                    return;
                }

                if (isEdit)
                    await TimesheetService.UpdatePayrollAsync(model);
                else
                {
                    model.CreatedAt = DateTime.Now;
                    await TimesheetService.CreatePayrollAsync(model);
                }

                Navigation.NavigateTo("/timesheet/payroll/list");
            }
            catch (Exception ex)
            {
                hasError = true;
                errorMessage = ex.Message;
            }
        }

        protected void BackToList()
        {
            Navigation.NavigateTo("/timesheet/payroll/list");
        }
    }
}
