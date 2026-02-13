using Microsoft.AspNetCore.Components;
using QMCH.Models;
using QMCH.Services;

namespace QMCH.Components.Pages
{
    public partial class MedicalTrackerCreateEdit
    {
        [Inject]
        public IEmployeeService? EmployeeService { get; set; }

        [Inject]
        public NavigationManager? Navigation { get; set; }

        [SupplyParameterFromQuery]
        public int? Id { get; set; }

        private List<Employee> employees = new();
        private MedicalSchedule model = new();
        private bool isEdit = false;
        private int editId = 0;

        protected override async Task OnInitializedAsync()
        {
            await LoadEmployees();
            
            if (Id.HasValue && Id.Value > 0)
            {
                isEdit = true;
                editId = Id.Value;
                await LoadMedicalSchedule(editId);
            }
            else
            {
                model = new MedicalSchedule();
                model.Status = "Scheduled";
                model.CreatedAt = DateTime.UtcNow;
                model.CreatedBy = "CurrentUser";
            }
        }

        private async Task LoadEmployees()
        {
            employees = await EmployeeService.GetAllAsync();
        }

        private async Task LoadMedicalSchedule(int id)
        {
            var schedules = await EmployeeService.GetMedicalSchedulesAsync();
            var schedule = schedules.FirstOrDefault(m => m.Id == id);
            if (schedule != null)
            {
                model = schedule;
            }
        }

        private async Task HandleSubmit()
        {
            model.UpdatedAt = DateTime.UtcNow;

            if (isEdit)
            {
                await EmployeeService.UpdateMedicalScheduleAsync(model);
            }
            else
            {
                model.CreatedAt = DateTime.UtcNow;
                model.CreatedBy = "CurrentUser";
                await EmployeeService.CreateMedicalScheduleAsync(model);
            }

            Navigation.NavigateTo("/caregiver/medical/schedule/list");
        }

        private void Cancel()
        {
            Navigation.NavigateTo("/caregiver/medical/schedule/list");
        }
    }
}


