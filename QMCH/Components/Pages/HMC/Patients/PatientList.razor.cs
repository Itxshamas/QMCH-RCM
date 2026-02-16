using QMCH.Models;
using QMCH.Interfaces.Services;
using Microsoft.AspNetCore.Components;

namespace QMCH.Components.Pages.HMC.Patients
{
    public partial class PatientList
    {        private IEnumerable<Patient>? patients;
        private IEnumerable<Patient>? filteredPatients;
        private string searchTerm = "";
        private string statusFilter = "";
        private int totalPatients = 0;
        private int activePatients = 0;
        private bool isLoading = true;
        private bool hasError = false;
        private string errorMessage = "";

        protected override async Task OnInitializedAsync()
        {
            await LoadPatients();
        }

        private async Task LoadPatients()
        {
            try
            {
                isLoading = true;
                hasError = false;

                if (PatientService == null)
                {
                    throw new InvalidOperationException("PatientService is not injected");
                }

                patients = await PatientService.GetAllPatientsAsync();
                totalPatients = patients?.Count() ?? 0;
                activePatients = patients?.Count(p => p.Status == "Active") ?? 0;
                ApplyFilters();

                isLoading = false;
            }
            catch (Exception ex)
            {
                hasError = true;
                errorMessage = $"Failed to load patients: {ex.Message}";
                isLoading = false;
            }
        }

        private void ApplyFilters()
        {
            if (patients == null) return;

            filteredPatients = patients.Where(p =>
                (string.IsNullOrEmpty(searchTerm) ||
                    p.FullName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    p.PatientId.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(statusFilter) || p.Status == statusFilter)
            ).ToList();
        }

        private void CreateNew()
        {
            if (Navigation != null)
                Navigation.NavigateTo("/hmc/patients/create");
        }

        private void ViewDetails(int id)
        {
            if (Navigation != null)
                Navigation.NavigateTo($"/hmc/patients/details/{id}");
        }

        private void EditPatient(int id)
        {
            if (Navigation != null)
                Navigation.NavigateTo($"/hmc/patients/edit/{id}");
        }

        private async Task DeletePatient(int id)
        {
            try
            {
                if (PatientService != null)
                {
                    await PatientService.DeletePatientAsync(id);
                    await LoadPatients();
                }
            }
            catch (Exception ex)
            {
                hasError = true;
                errorMessage = $"Failed to delete patient: {ex.Message}";
            }
        }
    }
}
