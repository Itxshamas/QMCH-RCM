using QMCH.Models;
using QMCH.Services;
using Microsoft.AspNetCore.Components;

namespace QMCH.Components.Pages
{
    public partial class EmployeeList
    {
        private List<Employee> allItems = new();
        private List<Employee> filteredItems = new();
        private bool isLoading = true;
        private bool showFilters = false;
        private bool showDeleteConfirm = false;
        private int deleteConfirmId = 0;
        private string deleteConfirmName = string.Empty;

        private string searchTerm = string.Empty;
        private string filterDepartment = string.Empty;
        private string filterJobTitle = string.Empty;
        private string filterStatus = string.Empty;

        private System.Threading.Timer? searchTimer;

        protected override async Task OnInitializedAsync()
        {
            await RefreshData();
        }

        private async Task RefreshData()
        {
            isLoading = true;
            try
            {
                allItems = await EmployeeService.GetAllAsync();
                ApplyFilters();
            }
            finally
            {
                isLoading = false;
            }
        }

        private void HandleSearchDebounced()
        {
            searchTimer?.Dispose();
            searchTimer = new System.Threading.Timer(_ => 
            {
                ApplyFilters();
                InvokeAsync(StateHasChanged);
            }, null, 300, System.Threading.Timeout.Infinite);
        }

        private void OnFilterChanged(ChangeEventArgs? e = null)
        {
            ApplyFilters();
        }

        private void OnBusinessUnitChanged(ChangeEventArgs e)
        {
            ApplyFilters();
        }

        private void OnDepartmentChanged(ChangeEventArgs e)
        {
            filterDepartment = e.Value?.ToString() ?? string.Empty;
            ApplyFilters();
        }

        private void OnJobTitleChanged(ChangeEventArgs e)
        {
            filterJobTitle = e.Value?.ToString() ?? string.Empty;
            ApplyFilters();
        }

        private void OnPositionChanged(ChangeEventArgs e)
        {
            ApplyFilters();
        }

        private void OnCategoryChanged(ChangeEventArgs e)
        {
            ApplyFilters();
        }

        private void OnLicenseChanged(ChangeEventArgs e)
        {
            ApplyFilters();
        }

        private void OnCompetencyChanged(ChangeEventArgs e)
        {
            ApplyFilters();
        }

        private void OnContractStatusChanged(ChangeEventArgs e)
        {
            ApplyFilters();
        }

        private void OnNFCChanged(ChangeEventArgs e)
        {
            ApplyFilters();
        }

        private void OnBIOChanged(ChangeEventArgs e)
        {
            ApplyFilters();
        }

        private void OnStatusChanged(ChangeEventArgs e)
        {
            filterStatus = e.Value?.ToString() ?? string.Empty;
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            filteredItems = allItems.Where(e => 
            {
                var searchLower = searchTerm.ToLower();
                var nameMatch = string.IsNullOrWhiteSpace(searchTerm) || 
                    $"{e.FirstName} {e.LastName}".ToLower().Contains(searchLower) ||
                    (e.Email?.ToLower().Contains(searchLower) ?? false) ||
                    (e.JobTitle?.ToLower().Contains(searchLower) ?? false);

                var departmentMatch = string.IsNullOrWhiteSpace(filterDepartment) ||
                    (e.Department?.Contains(filterDepartment, StringComparison.OrdinalIgnoreCase) ?? false);

                var jobTitleMatch = string.IsNullOrWhiteSpace(filterJobTitle) ||
                    (e.JobTitle?.Contains(filterJobTitle, StringComparison.OrdinalIgnoreCase) ?? false);

                var statusMatch = string.IsNullOrWhiteSpace(filterStatus) ||
                    e.Status == filterStatus;

                return nameMatch && departmentMatch && jobTitleMatch && statusMatch;
            }).ToList();
        }

        private void ToggleFilters()
        {
            showFilters = !showFilters;
        }

        private void ClearFilters()
        {
            searchTerm = string.Empty;
            filterDepartment = string.Empty;
            filterJobTitle = string.Empty;
            filterStatus = string.Empty;
            ApplyFilters();
        }

        private void NavigateToCreate()
        {
            Navigation.NavigateTo("/caregiver/list/create");
        }

        private void NavigateToEdit(int id)
        {
            Navigation.NavigateTo($"/caregiver/list/create?id={id}");
        }

        private void ConfirmDelete(int id, string firstName, string lastName)
        {
            deleteConfirmId = id;
            deleteConfirmName = $"{firstName} {lastName}";
            showDeleteConfirm = true;
        }

        private void CancelDelete()
        {
            showDeleteConfirm = false;
            deleteConfirmId = 0;
            deleteConfirmName = string.Empty;
        }

        private async Task ConfirmDeleteAction()
        {
            try
            {
                await EmployeeService.DeleteAsync(deleteConfirmId);
                showDeleteConfirm = false;
                deleteConfirmId = 0;
                deleteConfirmName = string.Empty;
                await RefreshData();
            }
            catch (Exception)
            {
                // Error handling could be enhanced with toast notifications
            }
        }

        private string GetStatusBadgeClass(string status)
        {
            return status switch
            {
                "Active" => "badge-success",
                "Inactive" => "badge-secondary",
                "On Leave" => "badge-warning",
                "Terminated" => "badge-danger",
                _ => "badge-secondary"
            };
        }

        private string GetStatusClass(string status)
        {
            return status switch
            {
                "Active" => "status-active",
                "Inactive" => "status-inactive",
                "On Leave" => "status-leave",
                "Terminated" => "status-terminated",
                _ => "status-inactive"
            };
        }

        public void Dispose()
        {
            searchTimer?.Dispose();
        }
    }
}
