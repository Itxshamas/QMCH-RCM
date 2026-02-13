using Microsoft.AspNetCore.Components;
using QMCH.Models;
using QMCH.Services;

namespace QMCH.Components.Pages
{
    public partial class InterviewsCreateEdit
    {
        [Parameter]
        public int? Id { get; set; }

        protected Interview interview = new() { Status = "Scheduled", InterviewType = "Phone Screen" };
        protected bool isLoading = false;
        protected bool isEditMode = false;
        protected string timeInput = "";

        protected override async Task OnInitializedAsync()
        {
            if (Id.HasValue && Id.Value > 0)
            {
                isEditMode = true;
                isLoading = true;
                var item = await HRService.GetInterviewByIdAsync(Id.Value);
                if (item != null)
                {
                    interview = item;
                    timeInput = interview.ScheduledDateTime.ToString("HH:mm");
                }
                isLoading = false;
            }
            else
            {
                interview = new Interview 
                { 
                    Status = "Scheduled", 
                    InterviewType = "Phone Screen",
                    ScheduledDateTime = DateTime.Now.AddDays(1)
                };
                timeInput = DateTime.Now.AddHours(1).ToString("HH:mm");
            }
        }

        protected async Task SaveInterview()
        {
            try
            {
                if (!string.IsNullOrEmpty(timeInput) && DateTime.TryParse(timeInput, out var time))
                {
                    interview.ScheduledDateTime = interview.ScheduledDateTime.Date.Add(time.TimeOfDay);
                }

                if (isEditMode)
                {
                    await HRService.UpdateInterviewAsync(interview);
                }
                else
                {
                    await HRService.CreateInterviewAsync(interview);
                }

                Navigation.NavigateTo("/hr/aquisition/interview/schedule/list");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving interview: {ex.Message}");
            }
        }

        protected void CancelForm()
        {
            Navigation.NavigateTo("/hr/aquisition/interview/schedule/list");
        }
    }
}
