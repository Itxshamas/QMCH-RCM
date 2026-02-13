using Microsoft.EntityFrameworkCore;
using QMCH.Data;
using QMCH.Models;

namespace QMCH.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _db;
        public EmployeeService(ApplicationDbContext db) { _db = db; }

        public async Task<List<Employee>> GetAllAsync() => await _db.Employees.OrderByDescending(e => e.CreatedAt).ToListAsync();
        public async Task<Employee?> GetByIdAsync(int id) => await _db.Employees.FindAsync(id);
        public async Task CreateAsync(Employee emp) { emp.CreatedAt = DateTime.Now; _db.Employees.Add(emp); await _db.SaveChangesAsync(); }
        public async Task UpdateAsync(Employee emp) { emp.UpdatedAt = DateTime.Now; _db.Employees.Update(emp); await _db.SaveChangesAsync(); }
        public async Task DeleteAsync(int id) { var e = await _db.Employees.FindAsync(id); if (e != null) { _db.Employees.Remove(e); await _db.SaveChangesAsync(); } }
        public async Task<int> GetCountAsync() => await _db.Employees.CountAsync();

        public async Task<List<Employee>> SearchNursesAsync(string searchTerm, int? classificationId)
        {
            var q = _db.Employees.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchTerm))
                q = q.Where(e => (e.FirstName + " " + e.LastName).Contains(searchTerm) ||
                    (e.Skills ?? "").Contains(searchTerm) || (e.Certifications ?? "").Contains(searchTerm));
            if (classificationId.HasValue && classificationId > 0)
                q = q.Where(e => e.ClassificationId == classificationId);
            return await q.ToListAsync();
        }

        public async Task<List<Classification>> GetClassificationsAsync() => await _db.Classifications.OrderBy(c => c.Name).ToListAsync();
        public async Task CreateClassificationAsync(Classification item) { _db.Classifications.Add(item); await _db.SaveChangesAsync(); }
        public async Task DeleteClassificationAsync(int id) { var e = await _db.Classifications.FindAsync(id); if (e != null) { _db.Classifications.Remove(e); await _db.SaveChangesAsync(); } }

        public async Task<List<MedicalSchedule>> GetMedicalSchedulesAsync() => await _db.MedicalSchedules.OrderByDescending(m => m.ScheduledDate).ToListAsync();
        public async Task CreateMedicalScheduleAsync(MedicalSchedule item) { _db.MedicalSchedules.Add(item); await _db.SaveChangesAsync(); }
        public async Task UpdateMedicalScheduleAsync(MedicalSchedule item) { _db.MedicalSchedules.Update(item); await _db.SaveChangesAsync(); }
        public async Task DeleteMedicalScheduleAsync(int id) { var e = await _db.MedicalSchedules.FindAsync(id); if (e != null) { _db.MedicalSchedules.Remove(e); await _db.SaveChangesAsync(); } }


        public async Task<List<Attendance>> GetAttendanceAsync() => await _db.Attendances.OrderByDescending(a => a.Date).ToListAsync();
        public async Task CreateAttendanceAsync(Attendance item) { _db.Attendances.Add(item); await _db.SaveChangesAsync(); }

        public async Task<List<TrainingSession>> GetTrainingSessionsAsync() => await _db.TrainingSessions.OrderByDescending(t => t.SessionDate).ToListAsync();
        public async Task CreateTrainingSessionAsync(TrainingSession item) { _db.TrainingSessions.Add(item); await _db.SaveChangesAsync(); }
        public async Task DeleteTrainingSessionAsync(int id) { var e = await _db.TrainingSessions.FindAsync(id); if (e != null) { _db.TrainingSessions.Remove(e); await _db.SaveChangesAsync(); } }
    }
}
