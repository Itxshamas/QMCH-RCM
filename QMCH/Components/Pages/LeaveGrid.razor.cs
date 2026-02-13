using QMCH.Models;
using QMCH.Services;
using Microsoft.AspNetCore.Components;

namespace QMCH.Components.Pages
{
    public partial class LeaveGrid
    {
        [Inject]
        private IEmployeeService EmployeeService { get; set; } = null!;

        // Modal visibility states
        private bool showNewLeaveModal = false;
        private bool showFullDayOffModal = false;
        private bool showRejectModal = false;
        private bool showFilterPanel = false;

        // Data collections
        private List<Leave> allItems = new();
        private List<Leave> filteredItems = new();
        private List<Leave> paginatedItems = new();
        private List<string> employees = new();

        // Form models
        private Leave newLeaveForm = new();
        private FullDayOffForm fullDayForm = new();
        private bool[] selectedDays = new bool[7];

        // Filtering
        private string filterEmployee = "";
        private string filterLeaveType = "";
        private string filterStatus = "";
        private DateTime? filterStartDate = null;
        private DateTime? filterEndDate = null;
        private string searchTerm = "";

        // Pagination
        private int currentPage = 1;
        private int pageSize = 10;
        private int totalPages = 1;

        // Rejection handling
        private string rejectionReason = "";
        private int rejectionLeaveId = 0;

        // Loading state
        private bool isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            isLoading = true;
            try
            {
                allItems = await LeaveService.GetAllAsync();
                await ApplyFilters();
                await LoadEmployeesFromDatabase();
            }
            finally
            {
                isLoading = false;
            }
        }

        private async Task LoadEmployeesFromDatabase()
        {
            try
            {
                // Load all active employees from the database
                var allEmployees = await EmployeeService.GetAllAsync();
                employees = allEmployees
                    .Where(e => e.Status == "Active")
                    .Select(e => $"{e.FirstName} {e.LastName}")
                    .OrderBy(name => name)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading employees: {ex.Message}");
                // Fallback to extracting from existing leave records
                ExtractUniqueEmployees();
            }
        }

        private void ExtractUniqueEmployees()
        {
            employees = allItems
                .Select(x => x.EmployeeName)
                .Where(x => !string.IsNullOrEmpty(x))
                .Distinct()
                .OrderBy(x => x)
                .ToList();
        }

        private async Task ApplyFilters()
        {
            filteredItems = allItems.Where(x =>
            {
                // Employee filter
                if (!string.IsNullOrEmpty(filterEmployee) && x.EmployeeName != filterEmployee)
                    return false;

                // Leave type filter
                if (!string.IsNullOrEmpty(filterLeaveType) && x.LeaveType != filterLeaveType)
                    return false;

                // Status filter
                if (!string.IsNullOrEmpty(filterStatus) && x.Status != filterStatus)
                    return false;

                // Date range filter
                if (filterStartDate.HasValue && x.StartDate < filterStartDate.Value)
                    return false;

                if (filterEndDate.HasValue && x.EndDate > filterEndDate.Value)
                    return false;

                // Search term
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    var searchLower = searchTerm.ToLower();
                    if (!x.EmployeeName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) &&
                        !x.LeaveType.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) &&
                        (string.IsNullOrEmpty(x.Reason) || !x.Reason.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)))
                        return false;
                }

                return true;
            }).ToList();

            // Calculate pagination
            totalPages = (int)Math.Ceiling((double)filteredItems.Count / pageSize);
            if (totalPages == 0) totalPages = 1;
            currentPage = 1;
            UpdatePaginatedItems();
        }

        private void UpdatePaginatedItems()
        {
            paginatedItems = filteredItems
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        private void ClearFilters()
        {
            filterEmployee = "";
            filterLeaveType = "";
            filterStatus = "";
            filterStartDate = null;
            filterEndDate = null;
            searchTerm = "";
            showFilterPanel = false;
            ApplyFilters();
        }

        private void OpenNewLeaveModal()
        {
            newLeaveForm = new Leave
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
                LeaveType = "Day Off",
                Status = "Pending",
                RequestedDate = DateTime.Today
            };
            showNewLeaveModal = true;
        }

        private void OpenFullDayOffModal()
        {
            fullDayForm = new FullDayOffForm
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1)
            };
            selectedDays = new bool[7];
            showFullDayOffModal = true;
        }

        private async Task SaveNewLeave()
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(newLeaveForm.EmployeeName))
                {
                    return; // Validation will be shown by EditForm
                }

                if (newLeaveForm.StartDate == default || newLeaveForm.EndDate == default)
                {
                    return;
                }

                if (newLeaveForm.EndDate < newLeaveForm.StartDate)
                {
                    return;
                }

                // Ensure dates are set properly
                if (newLeaveForm.Id == 0)
                {
                    // Create new leave
                    newLeaveForm.RequestedDate = DateTime.Today;
                    newLeaveForm.CreatedAt = DateTime.Now;
                    await LeaveService.CreateAsync(newLeaveForm);
                }
                else
                {
                    // Update existing leave
                    newLeaveForm.UpdatedAt = DateTime.Now;
                    await LeaveService.UpdateAsync(newLeaveForm);
                }
                
                // Close modal and reload data
                showNewLeaveModal = false;
                
                // Reset form
                newLeaveForm = new Leave
                {
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(1),
                    LeaveType = "Day Off",
                    Status = "Pending",
                    RequestedDate = DateTime.Today
                };

                await LoadData();
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine($"Error saving leave: {ex.Message}");
            }
        }

        private async Task SaveFullDayOff()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(fullDayForm.EmployeeName))
                {
                    return;
                }

                if (fullDayForm.StartDate == default || fullDayForm.EndDate == default)
                {
                    return;
                }

                if (fullDayForm.EndDate < fullDayForm.StartDate)
                {
                    return;
                }

                // Create a leave entry for the full day off
                var leave = new Leave
                {
                    EmployeeName = fullDayForm.EmployeeName,
                    EmployeeId = 0,
                    LeaveType = "Day Off",
                    StartDate = fullDayForm.StartDate,
                    EndDate = fullDayForm.EndDate,
                    Status = "Approved",
                    RequestedDate = DateTime.Today,
                    CreatedAt = DateTime.Now,
                    Reason = $"Full Day Off - Selected Days: {string.Join(", ", GetSelectedDayNames())}"
                };

                await LeaveService.CreateAsync(leave);
                
                // Close modal and reload data
                showFullDayOffModal = false;
                
                // Reset form
                fullDayForm = new FullDayOffForm
                {
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(1)
                };
                selectedDays = new bool[7];

                await LoadData();
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine($"Error saving full day off: {ex.Message}");
            }
        }

        private string[] GetSelectedDayNames()
        {
            var days = new[] { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };
            return days.Where((_, i) => selectedDays[i]).ToArray();
        }

        private async Task UpdateStatus(int id, string status)
        {
            try
            {
                await LeaveService.UpdateStatusAsync(id, status);
                await LoadData();
            }
            catch
            {
                // Handle error
            }
        }

        private async Task ApproveLeave(int id)
        {
            await UpdateStatus(id, "Approved");
        }

        private void ShowRejectModal(int leaveId)
        {
            rejectionLeaveId = leaveId;
            rejectionReason = "";
            showRejectModal = true;
        }

        private async Task ConfirmReject()
        {
            try
            {
                await LeaveService.RejectAsync(rejectionLeaveId, "Admin", rejectionReason);
                showRejectModal = false;
                await LoadData();
            }
            catch
            {
                // Handle error
            }
        }

        private void CloseRejectModal()
        {
            showRejectModal = false;
            rejectionReason = "";
            rejectionLeaveId = 0;
        }

        private void CloseNewLeaveModal()
        {
            showNewLeaveModal = false;
            newLeaveForm = new Leave
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
                LeaveType = "Day Off",
                Status = "Pending",
                RequestedDate = DateTime.Today
            };
        }

        private void CloseFullDayOffModal()
        {
            showFullDayOffModal = false;
            fullDayForm = new FullDayOffForm
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1)
            };
            selectedDays = new bool[7];
        }

        private async Task Delete(int id)
        {
            try
            {
                await LeaveService.DeleteAsync(id);
                await LoadData();
            }
            catch
            {
                // Handle error
            }
        }

        private void EditLeave(Leave leave)
        {
            newLeaveForm = new Leave
            {
                Id = leave.Id,
                EmployeeName = leave.EmployeeName,
                EmployeeId = leave.EmployeeId,
                LeaveType = leave.LeaveType,
                StartDate = leave.StartDate,
                EndDate = leave.EndDate,
                Reason = leave.Reason,
                Status = leave.Status
            };
            showNewLeaveModal = true;
        }

        private string GetStatusClass(string status)
        {
            return status switch
            {
                "Approved" => "approved",
                "Rejected" => "rejected",
                "Pending" => "pending",
                _ => "default"
            };
        }

        private void NextPage()
        {
            if (currentPage < totalPages)
            {
                currentPage++;
                UpdatePaginatedItems();
            }
        }

        private void PreviousPage()
        {
            if (currentPage > 1)
            {
                currentPage--;
                UpdatePaginatedItems();
            }
        }

        private void GoToPage(int pageNum)
        {
            currentPage = pageNum;
            UpdatePaginatedItems();
        }
    }

    public class FullDayOffForm
    {
        public string EmployeeName { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool OddDaysOnly { get; set; }
        public bool EvenDaysOnly { get; set; }
    }
}
