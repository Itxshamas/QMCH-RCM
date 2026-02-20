using StaffModel = QMCH.Models.Staff;
using QMCH.Interfaces.Services;
using Microsoft.AspNetCore.Components;

namespace QMCH.Components.Pages.HMC.Staff
{
    public partial class StaffDetail
    {
        [Parameter]
        public int StaffId { get; set; }

        [Inject]
        private IStaffService StaffService { get; set; } = null!;

        [Inject]
        private NavigationManager Navigation { get; set; } = null!;

        private StaffModel? staff;
        private bool isLoading = true;
        private bool showDeleteConfirm = false;
        private string activeTab = "skills";

        protected override async Task OnInitializedAsync()
        {
            await LoadStaffDetails();
        }

        private async Task LoadStaffDetails()
        {
            isLoading = true;
            try
            {
                staff = await StaffService.GetStaffByIdAsync(StaffId);
                if (staff is null)
                {
                    Console.WriteLine($"Staff with ID {StaffId} not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading staff details: {ex.Message}");
            }
            finally
            {
                isLoading = false;
            }
        }

        private void NavigateToEdit()
        {
            Navigation.NavigateTo($"/hmc/staff/form/{StaffId}");
        }

        private void ConfirmDelete()
        {
            showDeleteConfirm = true;
        }

        private async Task ConfirmDeleteAction()
        {
            try
            {
                await StaffService.DeleteStaffAsync(StaffId);
                Navigation.NavigateTo("/hmc/staff/list");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting staff: {ex.Message}");
            }
        }

        private string GetStatusColor(string? status)
        {
            return status switch
            {
                "Active" => "#28a745",
                "Inactive" => "#6c757d",
                "On Leave" => "#ffc107",
                _ => "#17a2b8"
            };
        }
    }
}
