using QMCH.Models;
using QMCH.Services;
using Microsoft.AspNetCore.Components;

namespace QMCH.Components.Pages
{
    public partial class NurseSearch
    {
        [Inject]
        private IEmployeeService EmployeeService { get; set; } = null!;

        private List<Employee> allEmployees = new();
        private List<Employee> filteredResults = new();
        private bool isLoading = true;
        private string searchTerm = "";
        private int currentPage = 1;
        private int pageSize = 10;
        private int totalPages = 1;

        private SearchCriteria searchCriteria = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadAllEmployees();
        }

        private async Task LoadAllEmployees()
        {
            isLoading = true;
            try
            {
                allEmployees = await EmployeeService.GetAllAsync();
                // Filter to only active nurses/caregivers
                allEmployees = allEmployees
                    .Where(e => e.Status == "Active" && !string.IsNullOrEmpty(e.Position))
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading employees: {ex.Message}");
            }
            finally
            {
                isLoading = false;
            }
        }

        private async Task PerformSearch()
        {
            // Validate search criteria
            if (searchCriteria.StartDate == default || searchCriteria.EndDate == default)
            {
                return;
            }

            if (searchCriteria.EndDate < searchCriteria.StartDate)
            {
                return;
            }

            isLoading = true;
            try
            {
                // Filter employees based on search criteria
                filteredResults = allEmployees
                    .Where(e => ApplySearchFilters(e))
                    .ToList();

                currentPage = 1;
                CalculatePagination();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error performing search: {ex.Message}");
            }
            finally
            {
                isLoading = false;
            }
        }

        private bool ApplySearchFilters(Employee employee)
        {
            // Filter by search term if provided
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var searchLower = searchTerm.ToLower();
                if (!employee.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) &&
                    !employee.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) &&
                    (string.IsNullOrEmpty(employee.Position) || !employee.Position.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)))
                {
                    return false;
                }
            }

            // Filter by availability based on join date and contract dates
            if (employee.JoinDate.HasValue && employee.JoinDate.Value > searchCriteria.StartDate)
            {
                return false;
            }

            return true;
        }

        private void OnSearchTermChanged()
        {
            if (!string.IsNullOrEmpty(searchTerm) && filteredResults.Any())
            {
                filteredResults = filteredResults
                    .Where(e => e.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                               e.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                               (e.Position != null && e.Position.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                currentPage = 1;
                CalculatePagination();
            }
        }

        private void ClearSearch()
        {
            searchCriteria = new();
            searchTerm = "";
            filteredResults = new();
            currentPage = 1;
            totalPages = 1;
        }

        private void OnPageSizeChanged(ChangeEventArgs e)
        {
            if (int.TryParse(e.Value?.ToString(), out int newSize))
            {
                pageSize = newSize;
                currentPage = 1;
                CalculatePagination();
            }
        }

        private void CalculatePagination()
        {
            totalPages = filteredResults.Count > 0 
                ? (int)Math.Ceiling((double)filteredResults.Count / pageSize)
                : 1;
        }

        private List<Employee> GetPaginatedResults()
        {
            return filteredResults
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        private void GoToPage(int pageNum)
        {
            if (pageNum >= 1 && pageNum <= totalPages)
            {
                currentPage = pageNum;
            }
        }

        private void HandleStartTimeChange(ChangeEventArgs e)
        {
            if (e.Value is string timeStr)
            {
                searchCriteria.StartTimeString = timeStr;
            }
        }

        private void HandleEndTimeChange(ChangeEventArgs e)
        {
            if (e.Value is string timeStr)
            {
                searchCriteria.EndTimeString = timeStr;
            }
        }

        private string GetEmployeeCode(int employeeId)
        {
            return $"QH-{employeeId:D3}";
        }

        private int CalculateAvailability(Employee employee)
        {
            // Calculate availability percentage based on employment status and contract
            if (employee.Status != "Active")
                return 0;

            // If employee joined after search start date, availability is reduced
            if (employee.JoinDate.HasValue && employee.JoinDate.Value > searchCriteria.StartDate)
            {
                var daysUnavailable = (employee.JoinDate.Value - searchCriteria.StartDate).Days;
                var totalSearchDays = (searchCriteria.EndDate - searchCriteria.StartDate).Days;
                
                if (totalSearchDays > 0)
                {
                    return Math.Max(0, 100 - (int)((daysUnavailable * 100) / totalSearchDays));
                }
            }

            return 100;
        }
    }

    public class SearchCriteria
    {
        public DateTime StartDate { get; set; } = DateTime.Today;
        public DateTime EndDate { get; set; } = DateTime.Today.AddDays(7);
        public string StartTimeString { get; set; } = "08:00";
        public string EndTimeString { get; set; } = "16:00";

        public TimeSpan StartTime => TimeSpan.Parse(StartTimeString);
        public TimeSpan EndTime => TimeSpan.Parse(EndTimeString);
    }
}
