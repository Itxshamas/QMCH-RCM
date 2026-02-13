using Microsoft.AspNetCore.Components;
using QMCH.Models;
using QMCH.Services;
using System.Text.Json;

namespace QMCH.Components.Pages
{
    public partial class ResumeBuilder
    {
        [SupplyParameterFromQuery]
        public int ApplicantId { get; set; }

        private Resume resumeModel = new();
        private List<ResumeEducation> educationList = new();
        private List<ResumeSkill> skillsList = new();
        private List<ResumeEmployment> employmentList = new();
        private List<ResumeReference> referencesList = new();
        private Applicant? currentApplicant;

        protected override async Task OnInitializedAsync()
        {
            if (ApplicantId > 0)
            {
                currentApplicant = await HRService.GetApplicantByIdAsync(ApplicantId);
                if (currentApplicant != null)
                {
                    resumeModel.ApplicantId = ApplicantId;
                }
            }

            // Initialize with one empty row for each section
            educationList.Add(new ResumeEducation());
            skillsList.Add(new ResumeSkill());
            employmentList.Add(new ResumeEmployment());
            referencesList.Add(new ResumeReference());
        }

        private void AddEducation()
        {
            educationList.Add(new ResumeEducation());
        }

        private void RemoveEducation(int index)
        {
            if (educationList.Count > 1)
            {
                educationList.RemoveAt(index);
            }
        }

        private void AddSkill()
        {
            skillsList.Add(new ResumeSkill());
        }

        private void RemoveSkill(int index)
        {
            if (skillsList.Count > 1)
            {
                skillsList.RemoveAt(index);
            }
        }

        private void AddEmployment()
        {
            employmentList.Add(new ResumeEmployment());
        }

        private void RemoveEmployment(int index)
        {
            if (employmentList.Count > 1)
            {
                employmentList.RemoveAt(index);
            }
        }

        private void AddReference()
        {
            referencesList.Add(new ResumeReference());
        }

        private void RemoveReference(int index)
        {
            if (referencesList.Count > 1)
            {
                referencesList.RemoveAt(index);
            }
        }

        private async Task SaveResume()
        {
            try
            {
                // Filter out empty entries
                var filteredEducation = educationList.Where(e => !string.IsNullOrEmpty(e.Degree) || !string.IsNullOrEmpty(e.Institution)).ToList();
                var filteredSkills = skillsList.Where(s => !string.IsNullOrEmpty(s.SkillName)).ToList();
                var filteredEmployment = employmentList.Where(e => !string.IsNullOrEmpty(e.Company) || !string.IsNullOrEmpty(e.JobTitle)).ToList();
                var filteredReferences = referencesList.Where(r => !string.IsNullOrEmpty(r.Name)).ToList();

                // Serialize to JSON
                resumeModel.EducationJson = JsonSerializer.Serialize(filteredEducation);
                resumeModel.SkillsJson = JsonSerializer.Serialize(filteredSkills);
                resumeModel.EmploymentHistoryJson = JsonSerializer.Serialize(filteredEmployment);
                resumeModel.ReferencesJson = JsonSerializer.Serialize(filteredReferences);

                resumeModel.UpdatedAt = DateTime.Now;

                // Check if resume already exists
                var existingResume = await HRService.GetResumeByApplicantIdAsync(ApplicantId);
                
                if (existingResume != null)
                {
                    // Update existing resume
                    resumeModel.Id = existingResume.Id;
                    resumeModel.CreatedAt = existingResume.CreatedAt;
                    await HRService.UpdateResumeAsync(resumeModel);
                }
                else
                {
                    // Create new resume
                    await HRService.CreateResumeAsync(resumeModel);
                }

                // Navigate back to applicant form
                Navigation.NavigateTo($"/hr/aquisition/applicants/list/create?id={ApplicantId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving resume: {ex.Message}");
            }
        }
    }
}
