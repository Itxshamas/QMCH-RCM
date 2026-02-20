using QMCH.Models;

namespace QMCH.Interfaces.Repositories
{
    public interface IVisitRepository : IGenericRepository<Visit>
    {
        Task<Visit?> GetWithDetailsAsync(int id);
        Task<IEnumerable<Visit>> GetUpcomingVisitsAsync();
    }
}
