using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using QMCH.Models;
using QMCH.Services;

namespace QMCH.Components.Pages
{
    public partial class MedicalTracker
    {
        [Inject]
        public IJSRuntime? JSRuntime { get; set; }

        [Inject]
        public IEmployeeService? EmployeeService { get; set; }

        [Inject]
        public NavigationManager? Navigation { get; set; }

        private List<MedicalSchedule> allItems = new();
        private List<MedicalSchedule> filteredItems = new();
        private bool isLoading = true;
        private string searchTerm = string.Empty;
        private int pageSize = 10;
        private int currentPage = 1;
        private int totalPages = 1;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            isLoading = true;
            allItems = await EmployeeService.GetMedicalSchedulesAsync();
            filteredItems = new List<MedicalSchedule>(allItems);
            CalculatePagination();
            isLoading = false;
        }

        private void HandleSearch()
        {
            currentPage = 1;
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            filteredItems = allItems
                .Where(item => string.IsNullOrEmpty(searchTerm) ||
                               item.EmployeeName?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true ||
                               item.ExamType?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true ||
                               item.Provider?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true ||
                               item.Status?.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) == true)
                .ToList();

            CalculatePagination();
        }

        private void CalculatePagination()
        {
            totalPages = (int)Math.Ceiling((decimal)filteredItems.Count / pageSize);
            if (currentPage > totalPages && totalPages > 0)
                currentPage = totalPages;
        }

        private List<MedicalSchedule> GetPagedItems()
        {
            var skip = (currentPage - 1) * pageSize;
            return filteredItems.Skip(skip).Take(pageSize).ToList();
        }

        private void NextPage()
        {
            if (currentPage < totalPages)
                currentPage++;
        }

        private void PreviousPage()
        {
            if (currentPage > 1)
                currentPage--;
        }

        private void NavigateToCreate()
        {
            Navigation.NavigateTo("/caregiver/medical/schedule/list/create");
        }

        private void EditItem(int id)
        {
            Navigation.NavigateTo($"/caregiver/medical/schedule/list/create?id={id}");
        }

        private async Task DeleteItem(int id)
        {
            if (await JSRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to delete this record?"))
            {
                await EmployeeService.DeleteMedicalScheduleAsync(id);
                await LoadData();
            }
        }

        private void ExportToCSV()
        {
            var csv = "ID,First Name,Last Name,Email,Join Date,Module,Status\n";
            foreach (var item in filteredItems)
            {
                csv += $"{item.Id},{GetFirstName(item.EmployeeName)},{GetLastName(item.EmployeeName)},{item.Provider ?? "-"},{item.ScheduledDate:MMM dd yyyy},{item.ExamType},{item.Status}\n";
            }
            DownloadFile("medical_appointments.csv", csv, "text/csv");
        }

        private void ExportToPDF()
        {
            JSRuntime.InvokeVoidAsync("alert", "PDF export functionality requires additional configuration");
        }

        private void PrintTable()
        {
            JSRuntime.InvokeVoidAsync("window.print");
        }

        private void DownloadFile(string filename, string content, string contentType)
        {
            JSRuntime.InvokeVoidAsync("downloadFile", filename, content, contentType);
        }

        private string GetFirstName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName)) return "-";
            var parts = fullName.Split(' ');
            return parts.Length > 0 ? parts[0] : fullName;
        }

        private string GetLastName(string fullName)
        {
            if (string.IsNullOrEmpty(fullName)) return "-";
            var parts = fullName.Split(' ');
            return parts.Length > 1 ? parts[parts.Length - 1] : "-";
        }

        private string GetStatusClass(string status)
        {
            return status?.ToLower() switch
            {
                "scheduled" => "badge-info",
                "completed" => "badge-success",
                "pending" => "badge-warning",
                "cancelled" => "badge-danger",
                _ => "badge-secondary"
            };
        }
    }
}


