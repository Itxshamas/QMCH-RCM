using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using QMCH.Models;
using QMCH.Services;

namespace QMCH.Components.Pages
{
    public partial class ApplicantCreateEdit
    {
        [SupplyParameterFromQuery]
        public int? Id { get; set; }

        private Applicant model = new() { AppliedDate = DateTime.Today, Status = "Not Scheduled" };
        private bool isEdit;
        private List<JobOrder> jobOrders = new();
        private int selectedJobOrderId = 0;
        private List<Applicant> allApplicants = new();
        private int currentApplicantIndex = 0;
        private int totalApplicants = 0;
        
        private string? photoFileName;
        private string? resumeFileName;

        protected override async Task OnInitializedAsync()
        {
            jobOrders = await HRService.GetJobOrdersAsync();
            allApplicants = await HRService.GetApplicantsAsync();
            totalApplicants = allApplicants.Count;

            if (Id.HasValue)
            {
                var applicant = await HRService.GetApplicantByIdAsync(Id.Value);
                if (applicant != null)
                {
                    model = applicant;
                    isEdit = true;
                    selectedJobOrderId = applicant.JobOrderId;
                    currentApplicantIndex = allApplicants.FindIndex(a => a.Id == Id.Value);
                }
            }
            else
            {
                // Initialize new applicant with default values
                model.JobOrderId = 0;
                model.AgencyId = 0;
            }
        }

        private async Task Save()
        {
            // Update the model with selected values
            model.JobOrderId = selectedJobOrderId > 0 ? selectedJobOrderId : model.JobOrderId;
            
            try
            {
                if (isEdit)
                {
                    await HRService.UpdateApplicantAsync(model);
                }
                else
                {
                    await HRService.CreateApplicantAsync(model);
                }
                Navigation.NavigateTo("/hr/aquisition/applicants/list");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving applicant: {ex.Message}");
            }
        }

        private void Cancel()
        {
            Navigation.NavigateTo("/hr/aquisition/applicants/list");
        }

        private Task HandlePhotoUpload(InputFileChangeEventArgs e)
        {
            // Handle photo upload - store file name or path
            if (e.File != null)
            {
                photoFileName = e.File.Name;
                model.PhotoUrl = e.File.Name;
            }
            return Task.CompletedTask;
        }

        private Task HandleResumeUpload(InputFileChangeEventArgs e)
        {
            // Handle resume upload - store file name or path
            if (e.File != null)
            {
                resumeFileName = e.File.Name;
                // In a real application, you would upload the file and store the URL
                model.ResumeUrl = e.File.Name;
            }
            return Task.CompletedTask;
        }

        private void PreviousApplicant()
        {
            if (currentApplicantIndex > 0)
            {
                currentApplicantIndex--;
                var applicant = allApplicants[currentApplicantIndex];
                Navigation.NavigateTo($"/hr/aquisition/applicants/list/create?id={applicant.Id}");
            }
        }

        private void NextApplicant()
        {
            if (currentApplicantIndex < allApplicants.Count - 1)
            {
                currentApplicantIndex++;
                var applicant = allApplicants[currentApplicantIndex];
                Navigation.NavigateTo($"/hr/aquisition/applicants/list/create?id={applicant.Id}");
            }
        }
    }
}
