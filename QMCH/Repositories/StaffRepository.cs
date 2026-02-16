using Microsoft.EntityFrameworkCore;
using QMCH.Data;
using QMCH.Interfaces.Repositories;
using QMCH.Models;

namespace QMCH.Repositories
{
    public class StaffRepository : GenericRepository<Staff>, IStaffRepository
    {
        public StaffRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Staff?> GetWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(s => s.Visits)
                .Include(s => s.StaffSkills)
                    .ThenInclude(ss => ss.Skill)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
