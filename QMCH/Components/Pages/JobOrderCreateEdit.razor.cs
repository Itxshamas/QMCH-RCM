using Microsoft.AspNetCore.Components;
using QMCH.Models;
using QMCH.Services;

namespace QMCH.Components.Pages
{
   
    public partial class JobOrderCreateEdit
    {
        private bool ValidateJobOrder()
        {
            var validationErrors = new List<string>();

            if (string.IsNullOrWhiteSpace(Model.Code))
                validationErrors.Add("Job Order Code is required.");

            if (string.IsNullOrWhiteSpace(Model.Title))
                validationErrors.Add("Job Title is required.");

            if (Model.NumberOfVisa <= 0)
                validationErrors.Add("Number of Visa must be greater than 0.");

            if (Model.DueDate == default(DateTime) || Model.DueDate < DateTime.Today)
                validationErrors.Add("Due Date must be today or in the future.");

            if (string.IsNullOrWhiteSpace(Model.JobDescription))
                validationErrors.Add("Job Description is required.");

            if (string.IsNullOrWhiteSpace(Model.RequiredQualification))
                validationErrors.Add("Required Qualification is required.");

            if (string.IsNullOrWhiteSpace(Model.ExperienceRange))
                validationErrors.Add("Experience Range is required.");

            if (string.IsNullOrWhiteSpace(Model.EmploymentStatus))
                validationErrors.Add("Employment Status is required.");

            if (string.IsNullOrWhiteSpace(Model.Priority))
                validationErrors.Add("Priority is required.");

            if (string.IsNullOrWhiteSpace(Model.RequiredSkills))
                validationErrors.Add("Required Skills is required.");

            if (validationErrors.Count > 0)
            {
                ErrorMessage = string.Join(" | ", validationErrors);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Clears all form data and resets to initial state
        /// </summary>
        public void ClearForm()
        {
            Model = new JobOrder
            {
                DueDate = DateTime.Today.AddDays(30),
                Priority = "Medium",
                Status = "Approved",
                EmploymentStatus = "Full Time"
            };
            IsFormDirty = false;
            UploadedFiles.Clear();
            AssignedAgents.Clear();
            SelectedAgent = string.Empty;
            AgentSearchTerm = string.Empty;
        }

        /// <summary>
        /// Gets filtered list of available agents based on search term
        /// </summary>
        /// <returns>List of available agents matching search criteria</returns>
        public List<string> GetFilteredAgents()
        {
            var available = AllAgents.Where(a => !AssignedAgents.Contains(a)).ToList();

            if (!string.IsNullOrWhiteSpace(AgentSearchTerm))
            {
                available = available
                    .Where(a => a.Contains(AgentSearchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return available;
        }

        /// <summary>
        /// Exports the current job order as JSON
        /// </summary>
        /// <returns>JSON string representation of the model</returns>
        public string ExportAsJson()
        {
            return System.Text.Json.JsonSerializer.Serialize(Model);
        }
    }
}
