using Microsoft.AspNetCore.Components;
using QMCH.Services;

namespace QMCH.Components.Pages
{
    public partial class Home
    {
        protected int clientCount;
        protected int employeeCount;
        protected bool IsModalOpen = false;
        protected ModalData CurrentModalData;

        protected string TodayDate =>
            DateTime.Now.ToString("dddd, MMMM dd, yyyy");

        // Modal data structure
        public class TableRow
        {
            public string EmployeeId { get; set; }
            public string Name { get; set; }
            public string Date { get; set; }
        }

        public class ModalData
        {
            public string Title { get; set; }
            public string ContentType { get; set; }
            public List<TableRow> TableData { get; set; }
            public List<string> ListItems { get; set; }
        }

        // Sample data for modals - Comprehensive dataset for all services
        private Dictionary<string, ModalData> ModalDataMap = new()
        {
            // ILS Modal
            {
                "ILS",
                new ModalData
                {
                    Title = "ILS",
                    ContentType = "ILS",
                    TableData = new List<TableRow>
                    {
                        new TableRow { EmployeeId = "QH-161", Name = "Carl Monica...", Date = "2026-11-13" }
                    }
                }
            },
            // PLS Modal
            {
                "PLS",
                new ModalData
                {
                    Title = "PLS",
                    ContentType = "PLS",
                    TableData = new List<TableRow>
                    {
                        new TableRow { EmployeeId = "QH-161", Name = "Carl Monica...", Date = "2026-11-13" }
                    },
                    ListItems = new List<string> { "ILS", "PLS" }
                }
            },
            // Competency Modal
            {
                "Competency",
                new ModalData
                {
                    Title = "Competency",
                    ContentType = "Table",
                    TableData = new List<TableRow>
                    {
                        new TableRow { EmployeeId = "QH-205", Name = "Sarah Johnson", Date = "2024-08-15" },
                        new TableRow { EmployeeId = "QH-187", Name = "Michael Brown", Date = "2024-09-22" },
                        new TableRow { EmployeeId = "QH-201", Name = "Jessica Davis", Date = "2024-07-10" }
                    }
                }
            },
            // Temporary License Modal
            {
                "Temporary Lic",
                new ModalData
                {
                    Title = "Temporary License",
                    ContentType = "Table",
                    TableData = new List<TableRow>
                    {
                        new TableRow { EmployeeId = "QH-363", Name = "Munvela Vel...", Date = "2024-06-28" },
                        new TableRow { EmployeeId = "QH-348", Name = "Honeylen C...", Date = "2024-05-29" },
                        new TableRow { EmployeeId = "QH-312", Name = "Esther Duque", Date = "2024-07-27" },
                        new TableRow { EmployeeId = "QH-248", Name = "Christine Jo...", Date = "2024-07-26" },
                        new TableRow { EmployeeId = "QH-242", Name = "Cynthia Lar...", Date = "2024-04-22" }
                    }
                }
            },
            // Permanent License Modal
            {
                "Permanent Lic",
                new ModalData
                {
                    Title = "Permanent License",
                    ContentType = "Table",
                    TableData = new List<TableRow>
                    {
                        new TableRow { EmployeeId = "QH-311", Name = "Mary Faith ...", Date = "2024-10-14" },
                        new TableRow { EmployeeId = "QH-298", Name = "Anilamol Vij...", Date = "2024-08-26" },
                        new TableRow { EmployeeId = "QH-297", Name = "Maria Chris...", Date = "2026-05-19" },
                        new TableRow { EmployeeId = "QH-288", Name = "Anna Leah ...", Date = "2021-10-13" },
                        new TableRow { EmployeeId = "QH-286", Name = "Michelle Ca...", Date = "2026-11-20" }
                    }
                }
            },
            // Prometric Modal
            {
                "Prometric",
                new ModalData
                {
                    Title = "Prometric",
                    ContentType = "Table",
                    TableData = new List<TableRow>
                    {
                        new TableRow { EmployeeId = "QH-161", Name = "Carl Monica...", Date = "2024-09-19" }
                    }
                }
            },
            // QID Modal
            {
                "QID",
                new ModalData
                {
                    Title = "QID",
                    ContentType = "Table",
                    TableData = new List<TableRow>
                    {
                        new TableRow { EmployeeId = "QH-152", Name = "Ahmed Hassan", Date = "2024-11-05" },
                        new TableRow { EmployeeId = "QH-198", Name = "Layla Ahmed...", Date = "2024-10-18" },
                        new TableRow { EmployeeId = "QH-175", Name = "Fatima Saleh", Date = "2024-09-30" }
                    }
                }
            },
            // Service Order Modal
            {
                "Service Order",
                new ModalData
                {
                    Title = "Service Order",
                    ContentType = "Table",
                    TableData = new List<TableRow>
                    {
                        new TableRow { EmployeeId = "QH-421", Name = "Robert Smith", Date = "2024-10-22" },
                        new TableRow { EmployeeId = "QH-439", Name = "Jennifer Lee...", Date = "2024-09-17" },
                        new TableRow { EmployeeId = "QH-445", Name = "David Wilson", Date = "2024-08-12" },
                        new TableRow { EmployeeId = "QH-428", Name = "Emily Taylor", Date = "2024-11-03" }
                    }
                }
            }
        };

        protected override async Task OnInitializedAsync()
        {
            clientCount = await ClientService.GetCountAsync();
            employeeCount = await EmployeeService.GetCountAsync();
        }

        protected void OpenModal(string serviceType)
        {
            if (ModalDataMap.TryGetValue(serviceType, out var data))
            {
                CurrentModalData = data;
                IsModalOpen = true;
            }
        }

        protected void CloseModal()
        {
            IsModalOpen = false;
            CurrentModalData = null;
        }

        public void Nav(string url)
        {
            Navigation.NavigateTo(url);
        }
    }
}


