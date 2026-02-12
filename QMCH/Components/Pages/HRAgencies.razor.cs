using QMCH.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace QMCH.Components.Pages
{
    public partial class HRAgencies
    {
        [Inject]
        private IJSRuntime JSRuntime { get; set; } = null!;

        private List<HRAgency>? allItems = new();
        private List<HRAgency>? filteredItems = new();
        private bool isLoading = true;
        private string searchTerm = string.Empty;
        private int currentPage = 1;
        private int pageSize = 10;
        private int totalPages = 1;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            isLoading = true;
            allItems = await HRService.GetAgenciesAsync();
            ApplyFilters();
            isLoading = false;
        }

        private void ApplyFilters()
        {
            if (allItems == null)
            {
                filteredItems = new();
                return;
            }

            filteredItems = allItems
                .Where(a => string.IsNullOrEmpty(searchTerm) ||
                           a.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                           (a.Country != null && a.Country.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) ||
                           (a.Email != null && a.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            totalPages = Math.Max(1, (int)Math.Ceiling((double)filteredItems.Count / pageSize));
            if (currentPage > totalPages)
                currentPage = totalPages;
        }

        private List<HRAgency> GetPaginatedItems()
        {
            if (filteredItems == null || filteredItems.Count == 0)
                return new();

            int skip = (currentPage - 1) * pageSize;
            return filteredItems.Skip(skip).Take(pageSize).ToList();
        }

        private void OnSearchChanged()
        {
            currentPage = 1;
            ApplyFilters();
        }

        private void OnPageSizeChanged(ChangeEventArgs e)
        {
            if (int.TryParse(e.Value?.ToString(), out int newSize))
            {
                pageSize = newSize;
                currentPage = 1;
                ApplyFilters();
            }
        }

        private void GoToPage(int pageNum)
        {
            if (pageNum >= 1 && pageNum <= totalPages)
            {
                currentPage = pageNum;
            }
        }

        private async Task Delete(int id)
        {
            if (await JSRuntime.InvokeAsync<bool>("confirm", "Are you sure you want to delete this agency?"))
            {
                await HRService.DeleteAgencyAsync(id);
                await LoadData();
            }
        }

        private async Task ToggleWeblogin(int id)
        {
            var agency = allItems?.FirstOrDefault(a => a.Id == id);
            if (agency != null)
            {
                agency.IsWebloginEnabled = !agency.IsWebloginEnabled;
                await HRService.UpdateAgencyAsync(agency);
                await LoadData();
            }
        }
    }
}
