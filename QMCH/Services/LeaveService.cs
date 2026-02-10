using Microsoft.EntityFrameworkCore;
using QMCH.Data;
using QMCH.Models;

namespace QMCH.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly ApplicationDbContext _db;
        public LeaveService(ApplicationDbContext db) { _db = db; }

        public async Task<List<Leave>> GetAllAsync() =>
            await _db.Leaves.OrderByDescending(l => l.RequestedDate).ToListAsync();

        public async Task<Leave?> GetByIdAsync(int id) =>
            await _db.Leaves.FindAsync(id);

        public async Task<Leave> CreateAsync(Leave leave)
        {
            leave.CreatedAt = DateTime.Now;
            _db.Leaves.Add(leave);
            await _db.SaveChangesAsync();
            return leave;
        }

        public async Task UpdateAsync(Leave leave)
        {
            leave.UpdatedAt = DateTime.Now;
            _db.Leaves.Update(leave);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var leave = await _db.Leaves.FindAsync(id);
            if (leave != null)
            {
                _db.Leaves.Remove(leave);
                await _db.SaveChangesAsync();
            }
        }

        public async Task UpdateStatusAsync(int id, string status)
        {
            var leave = await _db.Leaves.FindAsync(id);
            if (leave != null)
            {
                leave.Status = status;
                leave.ApprovedDate = DateTime.Now;
                await _db.SaveChangesAsync();
            }
        }

        public async Task<List<Leave>> GetByEmployeeAsync(int employeeId) =>
            await _db.Leaves.Where(l => l.EmployeeId == employeeId).OrderByDescending(l => l.StartDate).ToListAsync();

        public async Task ApproveAsync(int id, string approvedBy)
        {
            var leave = await _db.Leaves.FindAsync(id);
            if (leave != null)
            {
                leave.Status = "Approved";
                leave.ApprovedBy = approvedBy;
                leave.ApprovedDate = DateTime.Now;
                await _db.SaveChangesAsync();
            }
        }

        public async Task RejectAsync(int id, string rejectedBy, string reason)
        {
            var leave = await _db.Leaves.FindAsync(id);
            if (leave != null)
            {
                leave.Status = "Rejected";
                leave.ApprovedBy = rejectedBy; // Re-using ApprovedBy field for who rejected it
                leave.RejectionReason = reason;
                leave.ApprovedDate = DateTime.Now;
                await _db.SaveChangesAsync();
            }
        }
    }
}
