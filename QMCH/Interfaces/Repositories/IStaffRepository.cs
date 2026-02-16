using QMCH.Models;

namespace QMCH.Interfaces.Repositories
{
    public interface IStaffRepository : IGenericRepository<Staff>
    {
        Task<Staff?> GetWithDetailsAsync(int id);
    }
}
