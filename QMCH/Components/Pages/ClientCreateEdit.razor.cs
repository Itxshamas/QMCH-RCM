using Microsoft.AspNetCore.Components;
using QMCH.Services;
using QMCH.Models;

namespace QMCH.Components.Pages
{
    public partial class ClientCreateEdit : ComponentBase
    {
        [Inject] private IClientService _clientService { get; set; }
        [Inject] private NavigationManager _navigation { get; set; }

        [SupplyParameterFromQuery] public int? Id { get; set; }

        protected Client model = new();
        protected bool isEdit;

        protected override async Task OnInitializedAsync()
        {
            if (Id.HasValue)
            {
                var existing = await _clientService.GetByIdAsync(Id.Value);
                if (existing != null)
                {
                    model = existing;
                    isEdit = true;
                }
            }
            else
            {
                // Set default values for new client
                model.Status = "Active";
                model.Country = "Qatar";
                model.BillingCountry = "Qatar";
            }
        }

        protected async Task HandleSubmit()
        {
            if (isEdit)
                await _clientService.UpdateAsync(model);
            else
                await _clientService.CreateAsync(model);

            // Stay on the same page for editing
            StateHasChanged();
        }

        protected async Task HandleSubmitAndClose()
        {
            if (isEdit)
                await _clientService.UpdateAsync(model);
            else
                await _clientService.CreateAsync(model);

            _navigation.NavigateTo("/client/list");
        }

        protected void GoBack()
        {
            _navigation.NavigateTo("/client/list");
        }
    }
}