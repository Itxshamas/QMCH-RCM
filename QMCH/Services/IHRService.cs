using QMCH.Models;

namespace QMCH.Services
{
    public interface IHRService
    {
        Task<List<JobOrder>> GetJobOrdersAsync();
        Task<JobOrder?> GetJobOrderByIdAsync(int id);
        Task CreateJobOrderAsync(JobOrder item);
        Task UpdateJobOrderAsync(JobOrder item);
        Task DeleteJobOrderAsync(int id);

        Task<List<HRAgency>> GetAgenciesAsync();
        Task<HRAgency?> GetAgencyByIdAsync(int id);
        Task CreateAgencyAsync(HRAgency item);
        Task UpdateAgencyAsync(HRAgency item);
        Task DeleteAgencyAsync(int id);

        Task<List<Applicant>> GetApplicantsAsync();
        Task<Applicant?> GetApplicantByIdAsync(int id);
        Task CreateApplicantAsync(Applicant item);
        Task UpdateApplicantAsync(Applicant item);
        Task DeleteApplicantAsync(int id);

        Task<List<Interview>> GetInterviewsAsync();
        Task CreateInterviewAsync(Interview item);
        Task DeleteInterviewAsync(int id);
    }
}
