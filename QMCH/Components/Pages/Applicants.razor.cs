using QMCH.Models;
using QMCH.Services;
using Microsoft.AspNetCore.Components;

namespace QMCH.Components.Pages
{
    public partial class Applicants
    {
        private List<Applicant> allItems = new();
        private List<Applicant> filteredItems = new();
        private HashSet<int> selectedApplicants = new();
        private bool isLoading = true;
        private int currentPage = 1;
        private int pageSize = 10;
        private string searchText = string.Empty;
        private string statusFilter = string.Empty;
        private string candidateFilter = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            isLoading = true;
            allItems = await HRService.GetApplicantsAsync();
            ApplyFilters();
            isLoading = false;
        }

        private void ApplyFilters()
        {
            filteredItems = allItems.AsEnumerable()
                .Where(a => string.IsNullOrEmpty(statusFilter) || a.Status == statusFilter)
                .Where(a => string.IsNullOrEmpty(candidateFilter) || 
                    $"{a.FirstName} {a.LastName}".Equals(candidateFilter, StringComparison.OrdinalIgnoreCase))
                .Where(a => string.IsNullOrEmpty(searchText) || 
                    $"{a.FirstName} {a.LastName} {a.Email} {a.Phone}".Contains(searchText, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(a => a.AppliedDate)
                .ToList();
            
            currentPage = 1;
        }

        private void FilterByStatus(string? status)
        {
            statusFilter = status ?? string.Empty;
            ApplyFilters();
        }

        private void FilterByCandidate(string? candidate)
        {
            candidateFilter = candidate ?? string.Empty;
            ApplyFilters();
        }

        private void HandleSearch(string? key)
        {
            // Apply filters on every keystroke
            ApplyFilters();
        }

        private void SelectAllApplicants(bool selected)
        {
            if (selected)
            {
                selectedApplicants = new HashSet<int>(GetPaginatedItems().Select(a => a.Id));
            }
            else
            {
                selectedApplicants.Clear();
            }
        }

        private void ToggleApplicantSelection(int id, bool selected)
        {
            if (selected)
            {
                selectedApplicants.Add(id);
            }
            else
            {
                selectedApplicants.Remove(id);
            }
        }

        private async Task DeleteApplicant(int id)
        {
            if (await ConfirmDelete())
            {
                await HRService.DeleteApplicantAsync(id);
                await LoadData();
            }
        }

        private Task<bool> ConfirmDelete()
        {
            // In a real application, you would use a modal or confirmation dialog
            return Task.FromResult(true);
        }

        private string GetStatusClass(string status)
        {
            return status switch
            {
                "New" => "status-new",
                "Screening" => "status-screening",
                "Interview" => "status-interview",
                "Offer" => "status-offer",
                "Hired" => "status-hired",
                "Rejected" => "status-rejected",
                _ => "status-default"
            };
        }

        private List<Applicant> GetPaginatedItems()
        {
            return filteredItems
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        private int totalPages => (int)Math.Ceiling((double)filteredItems.Count / pageSize);

        private void GoToPage(int page)
        {
            if (page > 0 && page <= totalPages)
            {
                currentPage = page;
            }
        }

        private void previousPage()
        {
            if (currentPage > 1)
            {
                currentPage--;
            }
        }

        private void nextPage()
        {
            if (currentPage < totalPages)
            {
                currentPage++;
            }
        }
    }
}
