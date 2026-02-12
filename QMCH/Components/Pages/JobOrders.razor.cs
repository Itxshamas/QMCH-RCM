using Microsoft.AspNetCore.Components;
using QMCH.Models;

namespace QMCH.Components.Pages
{
    public partial class JobOrders
    {
        private List<JobOrder> items = new();
        private IEnumerable<JobOrder> filteredItems = Enumerable.Empty<JobOrder>();
        private string selectedStatus = "Approved";

        protected override Task OnInitializedAsync()
        {
            // TODO: Connect to HRService to load actual data
            // LoadData();
            
            // For now, start with empty list
            items = new List<JobOrder>();
            filteredItems = items;
            return Task.CompletedTask;
        }

        private void HandleNewClick()
        {
            Navigation.NavigateTo("/hr/aquisition/requisition/list/create");
        }

        private Task HandleSearch(ChangeEventArgs e)
        {
            var searchTerm = (string?)e.Value ?? string.Empty;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                filteredItems = items;
            }
            else
            {
                filteredItems = items.Where(x =>
                    x.Code.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    x.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    x.RaisedBy.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            return Task.CompletedTask;
        }

        private string GetPriorityBadge(string priority) => priority switch
        {
            "High" => "badge-danger",
            "Medium" => "badge-warning",
            "Low" => "badge-info",
            _ => "badge-info"
        };

        private string GetStatusBadge(string status) => status switch
        {
            "Approved" => "badge-success",
            "Pending" => "badge-warning",
            "Closed" => "badge-secondary",
            _ => "badge-info"
        };
    }
}
