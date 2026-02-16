using QMCH.Models;
using QMCH.Interfaces.Services;
using Microsoft.AspNetCore.Components;

namespace QMCH.Components.Pages.HMC.Patients
{
    public partial class PatientDetail
    {
        [Parameter]
        public int PatientId { get; set; }

        [Inject]
        private IPatientService PatientService { get; set; } = null!;

        [Inject]
        private NavigationManager Navigation { get; set; } = null!;

        private Patient? patient;
        private bool isLoading = true;
        private bool showDeleteConfirm = false;
        private string activeTab = "visits";

        protected override async Task OnInitializedAsync()
        {
            await LoadPatientDetails();
        }

        private async Task LoadPatientDetails()
        {
            isLoading = true;
            try
            {
                patient = await PatientService.GetPatientByIdAsync(PatientId);
                if (patient is null)
                {
                    Console.WriteLine($"Patient with ID {PatientId} not found");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading patient details: {ex.Message}");
            }
            finally
            {
                isLoading = false;
            }
        }

        private void NavigateToEdit()
        {
            Navigation.NavigateTo($"/hmc/patients/form/{PatientId}");
        }

        private void ConfirmDelete()
        {
            showDeleteConfirm = true;
        }

        private async Task ConfirmDeleteAction()
        {
            try
            {
                await PatientService.DeletePatientAsync(PatientId);
                Navigation.NavigateTo("/hmc/patients/list");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting patient: {ex.Message}");
            }
        }

        private string GetStatusColor(string status)
        {
            return status switch
            {
                "Active" => "#28a745",
                "Inactive" => "#6c757d",
                "Discharged" => "#dc3545",
                _ => "#17a2b8"
            };
        }
    }
}
