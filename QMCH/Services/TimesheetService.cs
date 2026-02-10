using Microsoft.EntityFrameworkCore;
using QMCH.Data;
using QMCH.Models;

namespace QMCH.Services
{
    public class TimesheetService : ITimesheetService
    {
        private readonly ApplicationDbContext _db;
        public TimesheetService(ApplicationDbContext db) { _db = db; }

        public async Task<List<TimesheetPayroll>> GetPayrollsAsync() => await _db.TimesheetPayrolls.OrderByDescending(t => t.PeriodEnd).ToListAsync();
        public async Task<TimesheetPayroll?> GetPayrollByIdAsync(int id) => await _db.TimesheetPayrolls.FindAsync(id);
        public async Task CreatePayrollAsync(TimesheetPayroll item) { _db.TimesheetPayrolls.Add(item); await _db.SaveChangesAsync(); }
        public async Task UpdatePayrollAsync(TimesheetPayroll item) { _db.TimesheetPayrolls.Update(item); await _db.SaveChangesAsync(); }
        public async Task DeletePayrollAsync(int id) { var e = await _db.TimesheetPayrolls.FindAsync(id); if (e != null) { _db.TimesheetPayrolls.Remove(e); await _db.SaveChangesAsync(); } }

        public async Task<List<TimesheetBilling>> GetBillingsAsync() => await _db.TimesheetBillings.OrderByDescending(t => t.PeriodEnd).ToListAsync();
        public async Task<TimesheetBilling?> GetBillingByIdAsync(int id) => await _db.TimesheetBillings.FindAsync(id);
        public async Task CreateBillingAsync(TimesheetBilling item) { _db.TimesheetBillings.Add(item); await _db.SaveChangesAsync(); }
        public async Task UpdateBillingAsync(TimesheetBilling item) { _db.TimesheetBillings.Update(item); await _db.SaveChangesAsync(); }
        public async Task DeleteBillingAsync(int id) { var e = await _db.TimesheetBillings.FindAsync(id); if (e != null) { _db.TimesheetBillings.Remove(e); await _db.SaveChangesAsync(); } }
    }
}
