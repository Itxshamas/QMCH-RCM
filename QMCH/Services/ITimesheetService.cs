using QMCH.Models;

namespace QMCH.Services
{
    public interface ITimesheetService
    {
        Task<List<TimesheetPayroll>> GetPayrollsAsync();
        Task<TimesheetPayroll?> GetPayrollByIdAsync(int id);
        Task CreatePayrollAsync(TimesheetPayroll item);
        Task UpdatePayrollAsync(TimesheetPayroll item);
        Task DeletePayrollAsync(int id);

        Task<List<TimesheetBilling>> GetBillingsAsync();
        Task<TimesheetBilling?> GetBillingByIdAsync(int id);
        Task CreateBillingAsync(TimesheetBilling item);
        Task UpdateBillingAsync(TimesheetBilling item);
        Task DeleteBillingAsync(int id);
    }
}
