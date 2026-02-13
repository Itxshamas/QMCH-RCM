using QMCH.Models;
using QMCH.Services;
using Microsoft.AspNetCore.Components;

namespace QMCH.Components.Pages
{
    public partial class Calendar
    {
        // Services are injected via @inject directives in the Razor file

        // Calendar state
        private DateTime currentDate = DateTime.Today;
        private List<CalendarDay> calendarDays = new();
        private List<ScheduleItem> allSchedules = new();
        private List<ScheduleItem> filteredSchedules = new();

        // Filters
        private string selectedClassification = "";
        private string selectedServiceType = "";
        private string selectedNurse = "";

        // Data sources
        private List<Client> allClients = new();
        private List<Employee> allNurses = new();

        // Modal states
        private bool showAddScheduleModal = false;
        private bool showMassUpdateModal = false;
        private bool showDutyInterruptionModal = false;
        private bool showConfirmModal = false;

        // Form data
        private ScheduleItem newSchedule = new();
        private string scheduleStartTime = "08:00";
        private string scheduleEndTime = "16:00";
        private bool[] selectedDays = new bool[7];

        // Duty Interruption form data
        private string dutyInterruptionNurse = "";
        private DateTime dutyInterruptionStartDate = DateTime.Today;
        private DateTime dutyInterruptionEndDate = DateTime.Today;
        private bool[] dutyInterruptionDays = new bool[7];
        private bool includeOddDays = false;
        private bool includeEvenDays = false;

        // Mass Update form data
        private string massUpdateClient = "";
        private string massUpdateServiceOrder = "";
        private string massUpdatePeriod = "";
        private DateTime massUpdateStartDate = DateTime.Today;
        private DateTime massUpdateEndDate = DateTime.Today;
        private bool[] massUpdateDays = new bool[7];
        private bool massUpdateIncludeOddDays = false;
        private bool massUpdateIncludeEvenDays = false;

        // Confirm modal data
        private DateTime confirmViewDate = DateTime.Today;
        private Dictionary<int, bool> confirmedSchedules = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            GenerateCalendar();
            InitializeCheckboxes();
        }

        private void InitializeCheckboxes()
        {
            // Initialize all days as selected by default
            for (int i = 0; i < 7; i++)
            {
                selectedDays[i] = false;
                dutyInterruptionDays[i] = true;
                massUpdateDays[i] = true;
            }
        }

        private async Task LoadData()
        {
            try
            {
                // Load schedules
                allSchedules = await ScheduleService.GetAllAsync();
                filteredSchedules = allSchedules;

                // Load clients
                allClients = await ClientService.GetAllAsync();

                // Load nurses/employees
                var allEmployees = await EmployeeService.GetAllAsync();
                allNurses = allEmployees
                    .Where(e => e.Status == "Active")
                    .OrderBy(e => e.FirstName)
                    .ToList();

                ApplyFilters();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
        }

        private void GenerateCalendar()
        {
            calendarDays = new();
            var firstDay = new DateTime(currentDate.Year, currentDate.Month, 1);
            var startDate = firstDay.AddDays(-(int)firstDay.DayOfWeek);
            
            for (int i = 0; i < 42; i++)
            {
                var d = startDate.AddDays(i);
                calendarDays.Add(new CalendarDay
                {
                    Date = d,
                    IsCurrentMonth = d.Month == currentDate.Month
                });
            }
        }

        private void PrevMonth()
        {
            currentDate = currentDate.AddMonths(-1);
            GenerateCalendar();
        }

        private void NextMonth()
        {
            currentDate = currentDate.AddMonths(1);
            GenerateCalendar();
        }

        private void GoToToday()
        {
            currentDate = DateTime.Today;
            GenerateCalendar();
        }

        private void ApplyFilters()
        {
            filteredSchedules = allSchedules.Where(s =>
            {
                // Filter by classification (client)
                if (!string.IsNullOrEmpty(selectedClassification) && 
                    !s.ClientName?.Contains(selectedClassification, StringComparison.OrdinalIgnoreCase) == true)
                    return false;

                // Filter by service type
                if (!string.IsNullOrEmpty(selectedServiceType) && s.ScheduleType != selectedServiceType)
                    return false;

                // Filter by nurse
                if (!string.IsNullOrEmpty(selectedNurse) && 
                    !s.EmployeeName?.Contains(selectedNurse, StringComparison.OrdinalIgnoreCase) == true)
                    return false;

                return true;
            }).ToList();

            StateHasChanged();
        }

        private void ClearFilters()
        {
            selectedClassification = "";
            selectedServiceType = "";
            selectedNurse = "";
            ApplyFilters();
        }

        private void RefreshCalendar()
        {
            ApplyFilters();
            StateHasChanged();
        }

        private List<(string Title, string ColorClass)> GetEventsForDay(DateTime date)
        {
            return filteredSchedules
                .Where(s => s.StartDate.Date <= date.Date && s.EndDate.Date >= date.Date)
                .Select(s => (
                    Title: $"{s.EmployeeName} @ {s.ClientName}",
                    ColorClass: GetColorClass(s.ScheduleType)
                ))
                .ToList();
        }

        private string GetColorClass(string? scheduleType)
        {
            return scheduleType switch
            {
                "Planning Schedule" => "planning-schedule",
                "Actual Schedule" => "actual-schedule",
                "Daily Interruption" => "daily-interruption",
                "Device Interruption" => "device-interruption",
                "Leave" => "leave",
                "Appointment" => "appointment",
                "Change Occurred" => "change-occurred",
                "Reliever" => "reliever",
                "Shift" => "actual-schedule",
                "Visit" => "planning-schedule",
                "Training" => "appointment",
                _ => "planning-schedule"
            };
        }

        // Add Schedule Modal Methods
        private void OpenAddScheduleModal()
        {
            newSchedule = new ScheduleItem
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today,
                ScheduleType = "Shift",
                Status = "Scheduled"
            };
            scheduleStartTime = "08:00";
            scheduleEndTime = "16:00";
            selectedDays = new bool[7];
            showAddScheduleModal = true;
        }

        private void CloseAddScheduleModal()
        {
            showAddScheduleModal = false;
        }

        private async Task SaveAddSchedule()
        {
            try
            {
                // Validate
                if (string.IsNullOrEmpty(newSchedule.EmployeeName) || string.IsNullOrEmpty(newSchedule.ClientName))
                {
                    Console.WriteLine("Employee and Client are required");
                    return;
                }

                if (newSchedule.EndDate < newSchedule.StartDate)
                {
                    Console.WriteLine("End date must be after start date");
                    return;
                }

                // Set title
                newSchedule.Title = $"{newSchedule.EmployeeName} - {newSchedule.ClientName}";

                // Parse times
                if (TimeSpan.TryParse(scheduleStartTime, out var startTime))
                    newSchedule.StartTime = startTime;

                if (TimeSpan.TryParse(scheduleEndTime, out var endTime))
                    newSchedule.EndTime = endTime;

                // Create schedule
                await ScheduleService.CreateAsync(newSchedule);

                // Refresh
                await LoadData();
                GenerateCalendar();
                showAddScheduleModal = false;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving schedule: {ex.Message}");
            }
        }

        // Mass Update Modal Methods
        private void OpenMassUpdateModal()
        {
            massUpdateClient = "";
            massUpdateServiceOrder = "";
            massUpdatePeriod = "";
            massUpdateStartDate = DateTime.Today;
            massUpdateEndDate = DateTime.Today;
            massUpdateDays = new bool[7] { true, true, true, true, true, true, true };
            massUpdateIncludeOddDays = false;
            massUpdateIncludeEvenDays = false;
            showMassUpdateModal = true;
        }

        private void CloseMassUpdateModal()
        {
            showMassUpdateModal = false;
        }

        private async Task PerformMassUpdate()
        {
            try
            {
                if (string.IsNullOrEmpty(massUpdateClient))
                {
                    Console.WriteLine("Client is required");
                    return;
                }

                if (massUpdateEndDate < massUpdateStartDate)
                {
                    Console.WriteLine("End date must be after start date");
                    return;
                }

                // Get selected schedules for mass update
                var schedulesToUpdate = allSchedules
                    .Where(s => s.ClientName == massUpdateClient &&
                               s.StartDate.Date >= massUpdateStartDate.Date &&
                               s.StartDate.Date <= massUpdateEndDate.Date)
                    .ToList();

                // Update all selected schedules
                foreach (var schedule in schedulesToUpdate)
                {
                    schedule.Status = "Updated";
                    schedule.Notes = $"Mass updated: {massUpdateServiceOrder}";
                    await ScheduleService.UpdateAsync(schedule);
                }

                // Refresh
                await LoadData();
                GenerateCalendar();
                showMassUpdateModal = false;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error performing mass update: {ex.Message}");
            }
        }

        // Duty Interruption Modal Methods
        private void OpenDutyInterruptionModal()
        {
            dutyInterruptionNurse = "";
            dutyInterruptionStartDate = DateTime.Today;
            dutyInterruptionEndDate = DateTime.Today;
            dutyInterruptionDays = new bool[7] { true, true, true, true, true, true, true };
            includeOddDays = false;
            includeEvenDays = false;
            showDutyInterruptionModal = true;
        }

        private void CloseDutyInterruptionModal()
        {
            showDutyInterruptionModal = false;
        }

        private async Task SaveDutyInterruption()
        {
            try
            {
                if (string.IsNullOrEmpty(dutyInterruptionNurse))
                {
                    Console.WriteLine("Nurse is required");
                    return;
                }

                if (dutyInterruptionEndDate < dutyInterruptionStartDate)
                {
                    Console.WriteLine("End date must be after start date");
                    return;
                }

                // Create duty interruption schedule
                var dutyInterruption = new ScheduleItem
                {
                    Title = $"Duty Interruption - {dutyInterruptionNurse}",
                    EmployeeName = dutyInterruptionNurse,
                    ScheduleType = "Daily Interruption",
                    StartDate = dutyInterruptionStartDate,
                    EndDate = dutyInterruptionEndDate,
                    Status = "Approved",
                    Notes = "Duty Interruption created"
                };

                await ScheduleService.CreateAsync(dutyInterruption);

                // Refresh
                await LoadData();
                GenerateCalendar();
                showDutyInterruptionModal = false;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving duty interruption: {ex.Message}");
            }
        }

        private async Task ConfirmSchedules()
        {
            try
            {
                // Mark all pending schedules as confirmed
                var pendingSchedules = allSchedules
                    .Where(s => s.Status == "Pending" || s.Status == "Scheduled")
                    .ToList();

                foreach (var schedule in pendingSchedules)
                {
                    schedule.Status = "Confirmed";
                    await ScheduleService.UpdateAsync(schedule);
                }

                await LoadData();
                GenerateCalendar();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error confirming schedules: {ex.Message}");
            }
        }

        private async Task SearchSchedule()
        {
            // Implementation for searching schedules
            ApplyFilters();
            StateHasChanged();
        }

        // Schedule Confirm Modal Methods
        private void OpenConfirmModal()
        {
            confirmViewDate = DateTime.Today;
            confirmedSchedules.Clear();
            
            // Initialize checkboxes for existing schedules
            foreach (var schedule in allSchedules.Where(s => s.Status != "Confirmed"))
            {
                confirmedSchedules[schedule.Id] = false;
            }
            
            showConfirmModal = true;
        }

        private void CloseConfirmModal()
        {
            showConfirmModal = false;
        }

        private void PrevConfirmMonth()
        {
            confirmViewDate = confirmViewDate.AddMonths(-1);
        }

        private void NextConfirmMonth()
        {
            confirmViewDate = confirmViewDate.AddMonths(1);
        }

        private void GoToTodayConfirm()
        {
            confirmViewDate = DateTime.Today;
        }

        private List<ScheduleItem> GetConfirmableSchedules()
        {
            return allSchedules
                .Where(s => s.StartDate.Month == confirmViewDate.Month &&
                           s.StartDate.Year == confirmViewDate.Year &&
                           s.Status != "Confirmed")
                .OrderBy(s => s.StartDate)
                .ToList();
        }

        private async Task SaveConfirmations()
        {
            try
            {
                var schedulesToConfirm = allSchedules
                    .Where(s => confirmedSchedules.ContainsKey(s.Id) && confirmedSchedules[s.Id])
                    .ToList();

                foreach (var schedule in schedulesToConfirm)
                {
                    schedule.Status = "Confirmed";
                    await ScheduleService.UpdateAsync(schedule);
                }

                if (schedulesToConfirm.Any())
                {
                    await LoadData();
                    GenerateCalendar();
                }

                showConfirmModal = false;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving confirmations: {ex.Message}");
            }
        }

        private class CalendarDay
        {
            public DateTime Date { get; set; }
            public bool IsCurrentMonth { get; set; }
        }
    }
}
