using Microsoft.EntityFrameworkCore;
using QMCH.Data;
using QMCH.Models;

namespace QMCH.Services
{
    public class HRService : IHRService
    {
        private readonly ApplicationDbContext _db;
        public HRService(ApplicationDbContext db) { _db = db; }

        public async Task<List<JobOrder>> GetJobOrdersAsync() => await _db.JobOrders.OrderByDescending(j => j.PostedDate).ToListAsync();
        public async Task<JobOrder?> GetJobOrderByIdAsync(int id) => await _db.JobOrders.FindAsync(id);
        public async Task CreateJobOrderAsync(JobOrder item) { _db.JobOrders.Add(item); await _db.SaveChangesAsync(); }
        public async Task UpdateJobOrderAsync(JobOrder item) { _db.JobOrders.Update(item); await _db.SaveChangesAsync(); }
        public async Task DeleteJobOrderAsync(int id) { var e = await _db.JobOrders.FindAsync(id); if (e != null) { _db.JobOrders.Remove(e); await _db.SaveChangesAsync(); } }

        public async Task<List<HRAgency>> GetAgenciesAsync() => await _db.HRAgencies.OrderBy(a => a.Name).ToListAsync();
        public async Task<HRAgency?> GetAgencyByIdAsync(int id) => await _db.HRAgencies.FindAsync(id);
        public async Task CreateAgencyAsync(HRAgency item) { _db.HRAgencies.Add(item); await _db.SaveChangesAsync(); }
        public async Task UpdateAgencyAsync(HRAgency item) { _db.HRAgencies.Update(item); await _db.SaveChangesAsync(); }
        public async Task DeleteAgencyAsync(int id) { var e = await _db.HRAgencies.FindAsync(id); if (e != null) { _db.HRAgencies.Remove(e); await _db.SaveChangesAsync(); } }

        public async Task<List<Applicant>> GetApplicantsAsync() => await _db.Applicants.OrderByDescending(a => a.AppliedDate).ToListAsync();
        public async Task<Applicant?> GetApplicantByIdAsync(int id) => await _db.Applicants.FindAsync(id);
        public async Task CreateApplicantAsync(Applicant item) { _db.Applicants.Add(item); await _db.SaveChangesAsync(); }
        public async Task UpdateApplicantAsync(Applicant item) { _db.Applicants.Update(item); await _db.SaveChangesAsync(); }
        public async Task DeleteApplicantAsync(int id) { var e = await _db.Applicants.FindAsync(id); if (e != null) { _db.Applicants.Remove(e); await _db.SaveChangesAsync(); } }

        public async Task<List<Interview>> GetInterviewsAsync() => await _db.Interviews.OrderByDescending(i => i.ScheduledDateTime).ToListAsync();
        public async Task CreateInterviewAsync(Interview item) { _db.Interviews.Add(item); await _db.SaveChangesAsync(); }
        public async Task DeleteInterviewAsync(int id) { var e = await _db.Interviews.FindAsync(id); if (e != null) { _db.Interviews.Remove(e); await _db.SaveChangesAsync(); } }

        public async Task<Resume?> GetResumeByApplicantIdAsync(int applicantId) => await _db.Resumes.FirstOrDefaultAsync(r => r.ApplicantId == applicantId);
        public async Task CreateResumeAsync(Resume item) { _db.Resumes.Add(item); await _db.SaveChangesAsync(); }
        public async Task UpdateResumeAsync(Resume item) { _db.Resumes.Update(item); await _db.SaveChangesAsync(); }
        public async Task DeleteResumeAsync(int id) { var e = await _db.Resumes.FindAsync(id); if (e != null) { _db.Resumes.Remove(e); await _db.SaveChangesAsync(); } }
    }
}

