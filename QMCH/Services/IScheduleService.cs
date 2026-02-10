using QMCH.Models;

namespace QMCH.Services
{
    public interface IScheduleService
    {
        Task<List<ScheduleItem>> GetAllAsync();
        Task<List<ScheduleItem>> GetByDateRangeAsync(DateTime start, DateTime end);
        Task<ScheduleItem?> GetByIdAsync(int id);
        Task<ScheduleItem> CreateAsync(ScheduleItem item);
        Task UpdateAsync(ScheduleItem item);
        Task DeleteAsync(int id);
        Task<List<ScheduleItem>> GetByEmployeeAsync(int employeeId);
        Task<List<ScheduleItem>> GetByClientAsync(int clientId);
    }
}
