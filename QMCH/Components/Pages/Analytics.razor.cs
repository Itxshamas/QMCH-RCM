using Microsoft.AspNetCore.Components;
using System;
using System.Globalization;


namespace QMCH.Components.Pages
{
    public partial class AnalyticsBase : ComponentBase
    {
        protected DateTime CurrentDate { get; set; } = DateTime.Today;

        protected string CurrentMonthYearDisplay => CurrentDate.ToString("MMMM yyyy", CultureInfo.InvariantCulture);

        protected int DaysInCurrentMonth => DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month);

        protected override void OnInitialized()
        {
            // Initial render
        }

        protected double GetDayPresencePercent(DateTime day)
        {
            var present = 0;
            for (int s = 0; s < GetStaffCount(); s++)
            {
                var st = GetAttendanceStatus(day, s);
                if (st.CssClass == "cell-present") present++;
            }
            if (GetStaffCount() == 0) return 0;
            return (present * 100.0) / GetStaffCount();
        }

        protected bool showDayDetail = false;
        protected DateTime detailDay;
        protected List<(string Staff, string Status, string Time)> dayDetails = new();

        protected void DayClicked(DateTime day)
        {
            detailDay = day;
            dayDetails.Clear();
            for (int s = 0; s < GetStaffCount(); s++)
            {
                var st = GetAttendanceStatus(day, s);
                dayDetails.Add((GetStaffName(s), st.BadgeText, st.TimeInfo));
            }
            showDayDetail = true;
        }

        protected void CloseDayDetail() => showDayDetail = false;

        // KPI helpers
        protected int TotalStaff => GetStaffCount();
        protected double AveragePresencePercent
        {
            get
            {
                // rough synthetic metric
                var days = DaysInCurrentMonth;
                var presentCount = 0;
                for (int d = 1; d <= days; d++)
                {
                    var day = new DateTime(CurrentDate.Year, CurrentDate.Month, d);
                    for (int s = 0; s < GetStaffCount(); s++)
                    {
                        var st = GetAttendanceStatus(day, s);
                        if (st.CssClass == "cell-present") presentCount++;
                    }
                }
                var possible = days * GetStaffCount();
                if (possible == 0) return 0;
                return (presentCount * 100.0) / possible;
            }
        }

        protected int TotalAbsent
        {
            get
            {
                var count = 0;
                for (int d = 1; d <= DaysInCurrentMonth; d++)
                {
                    var day = new DateTime(CurrentDate.Year, CurrentDate.Month, d);
                    for (int s = 0; s < GetStaffCount(); s++)
                    {
                        var st = GetAttendanceStatus(day, s);
                        if (st.CssClass == "cell-absent") count++;
                    }
                }
                return count;
            }
        }

        protected string SelectedChartType { get; set; } = "Line";
        protected bool ShowGridlines { get; set; } = true;
        protected bool ShowWeekends { get; set; } = true;

        protected void ToggleGridlines() { ShowGridlines = !ShowGridlines; }
        protected void ToggleWeekends() { ShowWeekends = !ShowWeekends; }

        protected void ExportCsv()
        {
            // For now create a CSV string and copy to clipboard (browser will prompt).
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("Date,Staff,Status,Time");
            for (int d = 1; d <= DaysInCurrentMonth; d++)
            {
                var day = new DateTime(CurrentDate.Year, CurrentDate.Month, d);
                for (int s = 0; s < GetStaffCount(); s++)
                {
                    var st = GetAttendanceStatus(day, s);
                    sb.AppendLine($"{day:yyyy-MM-dd},{GetStaffName(s)},{st.BadgeText},{st.TimeInfo}");
                }
            }

            // JS interop could be used to download the CSV; for now place in clipboard
            try
            {
                var csv = sb.ToString();
                // Use Clipboard via navigator.clipboard if available - left as simple alert for now
                Console.WriteLine("CSV Export:\n" + csv.Substring(0, Math.Min(1000, csv.Length)));
            }
            catch { }
        }

        protected void GoToPreviousMonth()
        {
            CurrentDate = CurrentDate.AddMonths(-1);
            StateHasChanged();
        }

        protected void GoToNextMonth()
        {
            CurrentDate = CurrentDate.AddMonths(1);
            StateHasChanged();
        }

        protected void GoToToday()
        {
            CurrentDate = DateTime.Today;
            StateHasChanged();
        }

        // Sample staff data
        private readonly string[] staffNames = new[]
        {
            "John Smith", "Sarah Williams", "Michael Brown", "Emily Davis",
            "James Wilson", "Jessica Garcia", "David Martinez", "Jennifer Rodriguez",
            "Robert Anderson", "Lisa Taylor", "William Thomas", "Mary Moore"
        };

        private readonly string[] avatarUrls = new[]
        {
            "https://i.pravatar.cc/150?img=1",
            "https://i.pravatar.cc/150?img=2",
            "https://i.pravatar.cc/150?img=3",
            "https://i.pravatar.cc/150?img=4",
            "https://i.pravatar.cc/150?img=5",
            "https://i.pravatar.cc/150?img=6",
            "https://i.pravatar.cc/150?img=7",
            "https://i.pravatar.cc/150?img=8",
            "https://i.pravatar.cc/150?img=9",
            "https://i.pravatar.cc/150?img=10",
            "https://i.pravatar.cc/150?img=11",
            "https://i.pravatar.cc/150?img=12"
        };

        protected int GetStaffCount()
        {
            return staffNames.Length;
        }

        protected string GetStaffName(int index)
        {
            if (index < 0 || index >= staffNames.Length)
                return "Unknown";

            return staffNames[index];
        }

        protected string GetStaffAvatar(int index)
        {
            if (index < 0 || index >= avatarUrls.Length)
                return "https://i.pravatar.cc/150?img=0";

            return avatarUrls[index];
        }

        protected AttendanceStatus GetAttendanceStatus(DateTime day, int person)
        {
            // Generate pseudo-random but consistent status based on day and person
            // Combine day of year and person so status varies by day and person
            var seed = (day.DayOfYear + person) % 100;

            if (seed < 5)
            {
                // Day Off
                return new AttendanceStatus
                {
                    CssClass = "cell-dayoff",
                    BadgeClass = "badge-dayoff",
                    BadgeText = "Day Off",
                    TimeInfo = ""
                };
            }
            else if (seed < 10)
            {
                // Leave
                return new AttendanceStatus
                {
                    CssClass = "cell-leave",
                    BadgeClass = "badge-leave",
                    BadgeText = "Leave",
                    TimeInfo = ""
                };
            }
            else if (seed < 13)
            {
                // 24H Duty
                return new AttendanceStatus
                {
                    CssClass = "cell-duty",
                    BadgeClass = "badge-duty",
                    BadgeText = "24H Duty",
                    TimeInfo = "8:00 AM - 8:00 AM"
                };
            }
            else if (seed < 16)
            {
                // Reliever
                return new AttendanceStatus
                {
                    CssClass = "cell-reliever",
                    BadgeClass = "badge-reliever",
                    BadgeText = "Reliever",
                    TimeInfo = "2:00 PM - 10:00 PM"
                };
            }
            else if (seed < 20)
            {
                // Absent
                return new AttendanceStatus
                {
                    CssClass = "cell-absent",
                    BadgeClass = "badge-absent",
                    BadgeText = "Absent",
                    TimeInfo = ""
                };
            }
            else
            {
                // Present (most common)
                var checkIn = $"{7 + (seed % 3)}:{((seed % 6) * 10):00} AM";
                var checkOut = $"{4 + (seed % 2)}:{((seed % 6) * 10):00} PM";

                return new AttendanceStatus
                {
                    CssClass = "cell-present",
                    BadgeClass = "badge-present",
                    BadgeText = "Present",
                    TimeInfo = $"{checkIn} - {checkOut}"
                };
            }
        }

        protected class AttendanceStatus
        {
            public string CssClass { get; set; } = "";
            public string BadgeClass { get; set; } = "";
            public string BadgeText { get; set; } = "";
            public string TimeInfo { get; set; } = "";
        }
    }
}