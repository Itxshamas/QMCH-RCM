using Microsoft.EntityFrameworkCore;
using QMCH.Data;
using QMCH.Interfaces.Repositories;
using QMCH.Models;

namespace QMCH.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Patient?> GetWithDetailsAsync(int id)
        {
            return await _dbSet
                .Include(p => p.Visits)
                .Include(p => p.Insurances)
                .Include(p => p.Medications)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Patient>> GetActivePatientsAsync()
        {
            return await _dbSet
                .Where(p => p.Status == "Active")
                .ToListAsync();
        }
    }
}
