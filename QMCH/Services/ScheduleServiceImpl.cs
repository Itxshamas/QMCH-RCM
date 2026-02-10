using Microsoft.EntityFrameworkCore;
using QMCH.Data;
using QMCH.Models;

namespace QMCH.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly ApplicationDbContext _context;

        public ScheduleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ScheduleItem>> GetAllAsync() =>
            await _context.ScheduleItems.OrderBy(s => s.StartDate).ToListAsync();

        public async Task<List<ScheduleItem>> GetByDateRangeAsync(DateTime start, DateTime end) =>
            await _context.ScheduleItems
                .Where(s => s.StartDate >= start && s.StartDate <= end)
                .OrderBy(s => s.StartDate)
                .ToListAsync();

        public async Task<ScheduleItem?> GetByIdAsync(int id) =>
            await _context.ScheduleItems.FindAsync(id);

        public async Task<ScheduleItem> CreateAsync(ScheduleItem item)
        {
            item.CreatedAt = DateTime.UtcNow;
            _context.ScheduleItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task UpdateAsync(ScheduleItem item)
        {
            item.UpdatedAt = DateTime.UtcNow;
            _context.ScheduleItems.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var s = await _context.ScheduleItems.FindAsync(id);
            if (s != null) { _context.ScheduleItems.Remove(s); await _context.SaveChangesAsync(); }
        }

        public async Task<List<ScheduleItem>> GetByEmployeeAsync(int employeeId) =>
            await _context.ScheduleItems
                .Where(s => s.EmployeeId == employeeId)
                .OrderBy(s => s.StartDate)
                .ToListAsync();

        public async Task<List<ScheduleItem>> GetByClientAsync(int clientId) =>
            await _context.ScheduleItems
                .Where(s => s.ClientId == clientId)
                .OrderBy(s => s.StartDate)
                .ToListAsync();
    }
}
