using Microsoft.AspNetCore.Components;
using QMCH.Models;
using QMCH.Services;

namespace QMCH.Components.Pages
{
    public partial class Classifications : ComponentBase
    {

        protected List<Classification> items = new();
        protected List<Classification> filteredItems = new();
        protected List<Classification> pagedItems = new();
        protected HashSet<int> selectedIds = new();
        protected List<string> availableJobTitles = new();

        protected bool isLoading = true;
        protected bool showQuickAdd = false;

        protected Classification newItem = new();
        protected string selectedJobTitle = string.Empty;
        protected string searchText = string.Empty;

        // Pagination
        protected int pageSize = 10;
        protected int currentPage = 1;

        protected int TotalPages => (int)Math.Ceiling((double)filteredItems.Count / pageSize);
        protected bool IsAllSelected => selectedIds.Count > 0 && selectedIds.Count == pagedItems.Count;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        protected async Task LoadData()
        {
            isLoading = true;
            items = await EmployeeService.GetClassificationsAsync();
            await LoadAvailableJobTitles();
            ApplyFilterAndPaging();
            isLoading = false;
        }

        protected async Task LoadAvailableJobTitles()
        {
            try
            {
                var employees = await EmployeeService.GetAllAsync();
                availableJobTitles = employees
                    .Where(e => !string.IsNullOrEmpty(e.JobTitle))
                    .Select(e => e.JobTitle)
                    .Distinct()
                    .OrderBy(x => x)
                    .ToList() ?? new List<string>();
            }
            catch
            {
                availableJobTitles = new List<string>();
            }
        }

        protected void ApplyFilterAndPaging()
        {
            filteredItems = new List<Classification>(items);

            // Apply search filter
            if (!string.IsNullOrEmpty(searchText))
            {
                filteredItems = filteredItems
                    .Where(x => (x.Name?.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                                (x.Description?.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false))
                    .ToList();
            }

            // Apply job title filter
            if (!string.IsNullOrEmpty(selectedJobTitle))
            {
                filteredItems = filteredItems
                    .Where(x => x.Name == selectedJobTitle)
                    .ToList();
            }

            currentPage = 1;
            UpdatePagedItems();
        }

        protected void UpdatePagedItems()
        {
            pagedItems = filteredItems
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        protected void ToggleQuickAdd()
        {
            showQuickAdd = !showQuickAdd;
            if (!showQuickAdd)
            {
                newItem = new();
            }
        }

        protected void CancelQuickAdd()
        {
            showQuickAdd = false;
            newItem = new();
        }

        protected async Task HandleSave()
        {
            if (string.IsNullOrEmpty(newItem.Name))
                return;

            await EmployeeService.CreateClassificationAsync(newItem);
            newItem = new();
            showQuickAdd = false;
            await LoadData();
        }

        protected void GoToMassCreate()
        {
            Navigation.NavigateTo("/caregiver/classifications/list/create/mass");
        }

        protected async Task DeleteSelected()
        {
            foreach (var id in selectedIds)
            {
                await EmployeeService.DeleteClassificationAsync(id);
            }
            selectedIds.Clear();
            await LoadData();
        }

        protected async Task DeleteSingle(int id)
        {
            if (await ConfirmDelete())
            {
                await EmployeeService.DeleteClassificationAsync(id);
                await LoadData();
            }
        }

        protected void ShowEdit(Classification item)
        {
            // Placeholder for edit functionality
        }

        protected void ShowSearch(Classification item)
        {
            // Placeholder for search/view functionality
        }

        protected void ToggleSelectAll(ChangeEventArgs e)
        {
            bool isChecked = (bool)e.Value!;
            if (isChecked)
            {
                selectedIds = new HashSet<int>(pagedItems.Select(x => x.Id));
            }
            else
            {
                selectedIds.Clear();
            }
        }

        protected void ToggleSelectItem(int id)
        {
            if (selectedIds.Contains(id))
                selectedIds.Remove(id);
            else
                selectedIds.Add(id);
        }

        protected void FilterByJobTitle(ChangeEventArgs e)
        {
            selectedJobTitle = e.Value?.ToString() ?? string.Empty;
            ApplyFilterAndPaging();
        }

        protected void ChangePageSize(ChangeEventArgs e)
        {
            pageSize = int.Parse(e.Value?.ToString() ?? "10");
            ApplyFilterAndPaging();
        }

        protected void NextPage()
        {
            if (currentPage < TotalPages)
            {
                currentPage++;
                UpdatePagedItems();
            }
        }

        protected void PreviousPage()
        {
            if (currentPage > 1)
            {
                currentPage--;
                UpdatePagedItems();
            }
        }

        protected void GoToPage(int pageNum)
        {
            if (pageNum >= 1 && pageNum <= TotalPages)
            {
                currentPage = pageNum;
                UpdatePagedItems();
            }
        }

        protected async Task<bool> ConfirmDelete()
        {
            return await Task.FromResult(true); // In real implementation, use JS interop for confirmation
        }
    }
}
