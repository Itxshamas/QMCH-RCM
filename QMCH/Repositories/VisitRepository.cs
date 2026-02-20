using Microsoft.EntityFrameworkCore;
using QMCH.Data;
using QMCH.Interfaces.Repositories;
using QMCH.Models;

namespace QMCH.Repositories
{
    public class VisitRepository : GenericRepository<Visit>, IVisitRepository
    {
        public VisitRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Visit?> GetWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(v => v.Patient)
                .Include(v => v.Staff)
                .Include(v => v.Documentations)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<Visit>> GetUpcomingVisitsAsync()
        {
            return await _dbSet
                .Include(v => v.Patient)
                .Include(v => v.Staff)
                .Where(v => v.ScheduledStartTime >= DateTime.Now && v.Status != "Cancelled")
                .OrderBy(v => v.ScheduledStartTime)
                .ToListAsync();
        }
    }
}
