using Microsoft.AspNetCore.Components;
using QMCH.Services;

namespace QMCH.Components.Pages
{
    public partial class Home
    {
        protected int clientCount;
        protected int employeeCount;

        protected string TodayDate =>
            DateTime.Now.ToString("dddd, MMMM dd, yyyy");

        protected override async Task OnInitializedAsync()
        {
            clientCount = await ClientService.GetCountAsync();
            employeeCount = await EmployeeService.GetCountAsync();
        }

        public void Nav(string url)
        {
            Navigation.NavigateTo(url);
        }
    }
}
