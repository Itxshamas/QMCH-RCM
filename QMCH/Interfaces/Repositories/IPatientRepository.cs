using QMCH.Models;

namespace QMCH.Interfaces.Repositories
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        Task<Patient?> GetWithDetailsAsync(int id);
        Task<IEnumerable<Patient>> GetActivePatientsAsync();
    }
}
