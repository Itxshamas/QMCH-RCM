using Microsoft.AspNetCore.Components;
using QMCH.Models;
using QMCH.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Components;
using System;

namespace QMCH.Components.Pages
{
    public partial class ClientTypes : ComponentBase
    {
        [Inject]
        protected IClientService ClientService { get; set; } = default!;
        [Inject]
        protected NavigationManager Navigation { get; set; } = default!;

        protected List<ClientType> items = new();
        protected List<ClientType> filteredItems = new();
        protected List<ClientType> pagedItems = new();
        protected HashSet<int> selectedIds = new();

        protected bool isLoading = true;

        protected bool showQuickAdd = false;

        protected ClientType newItem = new();

        protected ClientType? viewItem;
        protected ClientType? editItem;

        // paging & search
        protected int pageSize = 10;
        protected int currentPage = 1;
        protected string searchText = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        protected async Task LoadData()
        {
            isLoading = true;
            items = await ClientService.GetClientTypesAsync();
            ApplyFilterAndPaging();
            isLoading = false;
        }

        protected void ApplyFilterAndPaging()
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                filteredItems = items.ToList();
            }
            else
            {
                var txt = searchText.Trim().ToLowerInvariant();
                filteredItems = items.Where(x => (x.Description ?? string.Empty).ToLowerInvariant().Contains(txt)
                    || (x.ShortDescription ?? string.Empty).ToLowerInvariant().Contains(txt)).ToList();
            }

            if (pageSize <= 0) pageSize = 10;
            var totalPages = (int)Math.Ceiling((double)filteredItems.Count / pageSize);
            if (currentPage > totalPages) currentPage = Math.Max(1, totalPages);

            pagedItems = filteredItems.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        protected void ChangePageSize(ChangeEventArgs e)
        {
            if (int.TryParse(Convert.ToString(e.Value), out var newSize))
            {
                pageSize = newSize; currentPage = 1; ApplyFilterAndPaging();
            }
        }

        protected void PrevPage()
        {
            if (currentPage > 1) { currentPage--; ApplyFilterAndPaging(); }
        }

        protected void NextPage()
        {
            var total = (int)Math.Ceiling((double)filteredItems.Count / pageSize);
            if (currentPage < total) { currentPage++; ApplyFilterAndPaging(); }
        }

        protected bool IsAllSelected => pagedItems != null && pagedItems.All(p => selectedIds.Contains(p.Id));

        protected void ToggleSelectAll()
        {
            if (pagedItems == null) return;
            if (IsAllSelected)
            {
                foreach (var id in pagedItems.Select(p => p.Id)) selectedIds.Remove(id);
            }
            else
            {
                foreach (var id in pagedItems.Select(p => p.Id)) selectedIds.Add(id);
            }
        }

        protected void ToggleSelectItem(int id)
        {
            if (selectedIds.Contains(id)) selectedIds.Remove(id); else selectedIds.Add(id);
        }

        protected void ToggleQuickAdd() => showQuickAdd = !showQuickAdd;

        protected void CancelQuickAdd()
        {
            newItem = new ClientType(); showQuickAdd = false;
        }

        protected void GoToMassCreate() => Navigation.NavigateTo("/client/clienttypes/list/create/mass");

        protected async Task HandleSave()
        {
            if (newItem == null) return;
            await ClientService.CreateClientTypeAsync(newItem);
            newItem = new ClientType();
            showQuickAdd = false;
            await LoadData();
        }

        protected void ShowView(ClientType item)
        {
            viewItem = item;
        }

        protected void ShowEdit(ClientType item)
        {
            // create shallow copy to edit
            editItem = new ClientType { Id = item.Id, Description = item.Description, ShortDescription = item.ShortDescription };
        }

        protected async Task HandleUpdate()
        {
            if (editItem == null) return;

            // Fetch tracked entity, apply changes, then update to avoid detached-entity issues
            var entity = await ClientService.GetClientTypeByIdAsync(editItem.Id);
            if (entity != null)
            {
                entity.Description = editItem.Description;
                entity.ShortDescription = editItem.ShortDescription;
                await ClientService.UpdateClientTypeAsync(entity);
            }

            editItem = null;
            await LoadData();
        }

        protected async Task DeleteSingle(int id)
        {
            await ClientService.DeleteClientTypeAsync(id);
            selectedIds.Remove(id);
            await LoadData();
        }

        protected async Task DeleteSelected()
        {
            var toDelete = selectedIds.ToList();
            foreach (var id in toDelete)
            {
                await ClientService.DeleteClientTypeAsync(id);
            }
            selectedIds.Clear();
            await LoadData();
        }

        protected async Task Delete(int id)
        {
            await ClientService.DeleteClientTypeAsync(id);
            await LoadData();
        }
    }
}