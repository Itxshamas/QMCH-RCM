using Microsoft.AspNetCore.Components;
using QMCH.Models;
using QMCH.Services;

namespace QMCH.Components.Pages
{
    public partial class MassClassifications : ComponentBase
    {

        protected List<Classification> classifications = new();
        protected List<string> availableJobTitles = new();
        protected bool isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        protected async Task LoadData()
        {
            isLoading = true;
            
            // Load available job titles from employees
            await LoadAvailableJobTitles();
            
            // Initialize with 6 empty rows
            classifications = Enumerable.Range(0, 6)
                .Select(_ => new Classification())
                .ToList();
            
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

        protected void AddNewRow()
        {
            classifications.Add(new Classification());
            StateHasChanged();
        }

        protected void RemoveRow(int index)
        {
            if (index >= 0 && index < classifications.Count)
            {
                classifications.RemoveAt(index);
                StateHasChanged();
            }
        }

        protected async Task SaveAllClassifications()
        {
            try
            {
                // Filter out empty classifications (where Name is not set)
                var validClassifications = classifications
                    .Where(c => !string.IsNullOrEmpty(c.Name))
                    .ToList();

                if (!validClassifications.Any())
                {
                    // Show error message - in production, use a toast notification
                    return;
                }

                // Save each valid classification
                foreach (var classification in validClassifications)
                {
                    await EmployeeService.CreateClassificationAsync(classification);
                }

                // Navigate back to list
                Navigation.NavigateTo("/caregiver/classifications/list", forceLoad: false);
            }
            catch (Exception ex)
            {
                // Log error - in production, show a toast notification
                Console.WriteLine($"Error saving classifications: {ex.Message}");
            }
        }

        protected void GoBack()
        {
            Navigation.NavigateTo("/caregiver/classifications/list", forceLoad: false);
        }
    }
}
