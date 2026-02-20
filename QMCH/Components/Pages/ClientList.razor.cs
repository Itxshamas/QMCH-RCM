using Microsoft.AspNetCore.Components;
using QMCH.Services;
using QMCH.Models;

namespace QMCH.Components.Pages
{
    public partial class ClientList
    {
        [Inject] private IClientService ClientService { get; set; }
        [Inject] private NavigationManager Navigation { get; set; }

        // Data collections
        protected IEnumerable<Client> clients = Enumerable.Empty<Client>();
        private List<Client> allClients = new();
        private IEnumerable<Client> filteredClients = Enumerable.Empty<Client>();
        private IEnumerable<Client> paginatedClients = Enumerable.Empty<Client>();

        // Loading state
        protected bool isLoading = true;

        // Filter properties
        private string searchTerm = string.Empty;
        private string selectedArea = string.Empty;
        private string selectedType = string.Empty;
        private string selectedService = string.Empty;
        private string selectedStatus = string.Empty;

        // Pagination
        private int currentPage = 1;
        private int pageSize = 10;
        private int totalPages = 1;

        // Selection
        private HashSet<int> selectedClients = new();
        private bool selectAll = false;

        // Filter options - These should ideally come from your database
        private List<string> areas = new() {  };
        private List<string> clientTypes = new() { "Hammad Medical Corporation", "Home", "Clinic" , "School"  };
        private List<string> services = new() { "Pediatric", "Geriatric", "Private Nurse" };

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }
        protected string GetClientTypeById(int clientTypeId)
        {
            return clientTypeId switch
            {
                1 => "Home",
                2 => "Business",
                3 => "Corporate",
                _ => "Home",
            };
        }

        private async Task LoadData()
        {
            isLoading = true;
            try
            {
                allClients = await ClientService.GetAllAsync();
                clients = allClients;
                ApplyFilters();
            }
            finally
            {
                isLoading = false;
            }
        }

        private void ApplyFilters()
        {
            var filtered = allClients.AsEnumerable();

            // Apply search filter
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                filtered = filtered.Where(c =>
                    (c.FirstName ?? "").Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    (c.LastName ?? "").Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    (c.Email ?? "").Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    (c.Phone ?? "").Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    (c.ClientId ?? "").Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            // Apply area filter
            if (!string.IsNullOrWhiteSpace(selectedArea))
            {
                filtered = filtered.Where(c =>
                    (c.City ?? "").Equals(selectedArea, StringComparison.OrdinalIgnoreCase));
            }

            // Apply type filter
            if (!string.IsNullOrWhiteSpace(selectedType))
            {
                filtered = filtered.Where(c =>
                    GetClientTypeById(c.ClientTypeId).Equals(selectedType, StringComparison.OrdinalIgnoreCase));
            }

            // Apply service filter (if you have a Services property in your Client model)
            if (!string.IsNullOrWhiteSpace(selectedService))
            {
                // Implement service filtering based on your data model
                // filtered = filtered.Where(c => c.Services.Contains(selectedService));
            }

            // Apply status filter
            if (!string.IsNullOrWhiteSpace(selectedStatus))
            {
                filtered = filtered.Where(c =>
                    (c.Status ?? "Active").Equals(selectedStatus, StringComparison.OrdinalIgnoreCase));
            }

            filteredClients = filtered.ToList();

            // Calculate pagination
            totalPages = (int)Math.Ceiling(filteredClients.Count() / (double)pageSize);
            if (currentPage > totalPages && totalPages > 0)
            {
                currentPage = totalPages;
            }
            if (currentPage < 1)
            {
                currentPage = 1;
            }

            // Apply pagination
            paginatedClients = filteredClients
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            StateHasChanged();
        }

        protected Task HandleSearch()
        {
            currentPage = 1; // Reset to first page on search
            ApplyFilters();
            return Task.CompletedTask;
        }

        // Navigation methods
        protected void CreateNew() => Navigation.NavigateTo("/client/list/create");

        protected void Edit(int id) => Navigation.NavigateTo($"/client/list/create?id={id}");

        protected void View(int id)
        {
            // Navigate to view page or show modal
            Navigation.NavigateTo($"/client/list/view?id={id}");
        }

        protected async Task Delete(int id)
        {
            // Consider adding confirmation dialog
            await ClientService.DeleteAsync(id);
            await LoadData();
        }

        // Pagination methods
        private void GoToPage(int pageNumber)
        {
            if (pageNumber >= 1 && pageNumber <= totalPages)
            {
                currentPage = pageNumber;
                ApplyFilters();
            }
        }

        private void NextPage()
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                ApplyFilters();
            }
        }

        private void PreviousPage()
        {
            if (currentPage > 1)
            {
                currentPage--;
                ApplyFilters();
            }
        }

        private string GetShowingInfo()
        {
            if (!filteredClients.Any())
                return "0 to 0";

            int start = (currentPage - 1) * pageSize + 1;
            int end = Math.Min(currentPage * pageSize, filteredClients.Count());
            return $"{start} to {end}";
        }

        // Selection methods
        private void ToggleSelectAll(ChangeEventArgs e)
        {
            selectAll = (bool)e.Value;

            if (selectAll)
            {
                selectedClients = paginatedClients.Select(c => c.Id).ToHashSet();
            }
            else
            {
                selectedClients.Clear();
            }

            StateHasChanged();
        }

        private void ToggleClientSelection(int clientId)
        {
            if (selectedClients.Contains(clientId))
            {
                selectedClients.Remove(clientId);
            }
            else
            {
                selectedClients.Add(clientId);
            }

            selectAll = paginatedClients.All(c => selectedClients.Contains(c.Id));
            StateHasChanged();
        }
    }
}