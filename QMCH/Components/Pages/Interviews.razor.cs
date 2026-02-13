using Microsoft.AspNetCore.Components;
using QMCH.Models;
using QMCH.Services;

namespace QMCH.Components.Pages
{
    public partial class Interviews
    {
        protected List<Interview> items = new();
        protected List<Interview> filteredItems = new();
        protected List<Interview> paginatedItems = new();
        protected bool isLoading = true;
        protected string searchText = "";
        protected int pageSize = 10;
        protected int currentPage = 1;
        protected int totalPages = 1;
        protected string sortColumn = "";
        protected bool sortAscending = true;
        protected bool showDeleteConfirm = false;
        protected int deleteItemId = 0;
        protected string deleteItemName = "";

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        protected async Task LoadData()
        {
            isLoading = true;
            items = await HRService.GetInterviewsAsync();
            ApplyFiltersAndSort();
            isLoading = false;
        }

        protected void ApplyFiltersAndSort()
        {
            filteredItems = items;

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                filteredItems = filteredItems
                    .Where(i => (i.ApplicantName?.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                               (i.Email?.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                               (i.Position?.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                               (i.MobileNumber?.Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(sortColumn))
            {
                filteredItems = sortColumn switch
                {
                    "JobCode" => sortAscending 
                        ? filteredItems.OrderBy(i => i.JobOrderId).ToList()
                        : filteredItems.OrderByDescending(i => i.JobOrderId).ToList(),
                    _ => filteredItems
                };
            }

            totalPages = (int)Math.Ceiling((double)filteredItems.Count / pageSize);
            currentPage = 1;
            UpdatePaginatedItems();
        }

        protected void UpdatePaginatedItems()
        {
            paginatedItems = filteredItems
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        protected void OnSearchChanged()
        {
            ApplyFiltersAndSort();
        }

        protected void OnPageSizeChanged(ChangeEventArgs e)
        {
            if (int.TryParse(e.Value?.ToString(), out int size))
            {
                pageSize = size;
                ApplyFiltersAndSort();
            }
        }

        protected void ClickSort()
        {
            SortBy("JobCode");
        }

        protected void SortBy(string column)
        {
            if (sortColumn == column)
            {
                sortAscending = !sortAscending;
            }
            else
            {
                sortColumn = column;
                sortAscending = true;
            }
            ApplyFiltersAndSort();
        }

        protected void PreviousPage()
        {
            if (currentPage > 1)
            {
                currentPage--;
                UpdatePaginatedItems();
            }
        }

        protected void NextPage()
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                UpdatePaginatedItems();
            }
        }

        protected void NavigateToCreate()
        {
            Navigation.NavigateTo("/hr/aquisition/interview/schedule/list/create");
        }

        protected void NavigateToEdit(int id)
        {
            Navigation.NavigateTo($"/hr/aquisition/interview/schedule/list/edit/{id}");
        }

        protected void ConfirmDelete(int id, string? name)
        {
            deleteItemId = id;
            deleteItemName = name ?? "Unknown";
            showDeleteConfirm = true;
        }

        protected void CancelDelete()
        {
            showDeleteConfirm = false;
            deleteItemId = 0;
            deleteItemName = "";
        }

        protected async Task ConfirmDeleteAction()
        {
            await HRService.DeleteInterviewAsync(deleteItemId);
            showDeleteConfirm = false;
            await LoadData();
        }

        protected string GetStatusClass(string? status)
        {
            return status?.ToLower() switch
            {
                "scheduled" => "status-scheduled",
                "completed" => "status-completed",
                "cancelled" => "status-cancelled",
                "pending" => "status-pending",
                _ => "status-default"
            };
        }
    }
}
