using QMCH.Models;

namespace QMCH.Services
{
    public interface ILeaveService
    {
        Task<List<Leave>> GetAllAsync();
        Task<Leave?> GetByIdAsync(int id);
        Task<Leave> CreateAsync(Leave leave);
        Task UpdateAsync(Leave leave);
        Task DeleteAsync(int id);
        Task<List<Leave>> GetByEmployeeAsync(int employeeId);
        Task UpdateStatusAsync(int id, string status);
        Task ApproveAsync(int id, string approvedBy);
        Task RejectAsync(int id, string rejectedBy, string reason);
    }
}
