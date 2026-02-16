using QMCH.Interfaces.Repositories;
using QMCH.Interfaces.Services;
using QMCH.Models;

namespace QMCH.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _patientRepository.GetAllAsync();
        }

        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            return await _patientRepository.GetWithDetailsAsync(id);
        }

        public async Task CreatePatientAsync(Patient patient)
        {
            patient.CreatedAt = DateTime.Now;
            patient.UpdatedAt = DateTime.Now;
            await _patientRepository.AddAsync(patient);
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            patient.UpdatedAt = DateTime.Now;
            await _patientRepository.UpdateAsync(patient);
        }

        public async Task DeletePatientAsync(int id)
        {
            await _patientRepository.DeleteAsync(id);
        }

        public async Task<int> GetTotalPatientsCountAsync()
        {
            return await _patientRepository.CountAsync();
        }
    }
}
