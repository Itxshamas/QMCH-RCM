using Microsoft.AspNetCore.Components;
using QMCH.Services;
using QMCH.Models;

namespace QMCH.Components.Pages
{
    public partial class ClientView : ComponentBase
    {
        [Inject] private IClientService _clientService { get; set; }
        [Inject] private NavigationManager _navigation { get; set; }

        [SupplyParameterFromQuery] public int? id { get; set; }

        protected Client? client;
        protected bool isLoading = true;

        protected override async Task OnParametersSetAsync()
        {
            await LoadClient();
        }

        private async Task LoadClient()
        {
            isLoading = true;
            try
            {
                if (id.HasValue)
                {
                    client = await _clientService.GetByIdAsync(id.Value);
                }
                else
                {
                    client = null;
                }
            }
            finally
            {
                isLoading = false;
                StateHasChanged();
            }
        }

        protected void Back()
        {
            _navigation.NavigateTo("/client/list");
        }
    }
}
