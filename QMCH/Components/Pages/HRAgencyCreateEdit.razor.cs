using QMCH.Models;
using Microsoft.AspNetCore.Components;

namespace QMCH.Components.Pages
{
    public partial class HRAgencyCreateEdit
    {
        [SupplyParameterFromQuery]
        public int? Id { get; set; }

        private HRAgency model = new();
        private bool isEdit = false;
        private string activeTab = "agency";

        protected override async Task OnInitializedAsync()
        {
            if (Id.HasValue)
            {
                var agency = await HRService.GetAgencyByIdAsync(Id.Value);
                if (agency != null)
                {
                    model = agency;
                    isEdit = true;
                }
            }
        }

        private async Task HandleSave()
        {
            await Save();
        }

        private async Task Save()
        {
            try
            {
                if (isEdit)
                {
                    await HRService.UpdateAgencyAsync(model);
                }
                else
                {
                    model.CreatedAt = DateTime.Now;
                    await HRService.CreateAgencyAsync(model);
                }
                Navigation.NavigateTo("/hr/agency/list");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving agency: {ex.Message}");
            }
        }

        private List<string> GetCountries()
        {
            return new List<string>
            {
                "Philippines",
                "United States",
                "Canada",
                "United Kingdom",
                "Australia",
                "Germany",
                "France",
                "Japan",
                "India",
                "China",
                "Brazil",
                "Mexico",
                "Singapore",
                "Malaysia",
                "Thailand",
                "Vietnam",
                "Indonesia",
                "South Korea",
                "Taiwan",
                "Hong Kong",
                "Other"
            };
        }
    }
}
