using QMCH.Models;

namespace QMCH.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task CreateAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
        Task<int> GetCountAsync();
        Task<List<Employee>> SearchNursesAsync(string searchTerm, int? classificationId);

        Task<List<Classification>> GetClassificationsAsync();
        Task CreateClassificationAsync(Classification item);
        Task DeleteClassificationAsync(int id);

        Task<List<MedicalSchedule>> GetMedicalSchedulesAsync();
        Task CreateMedicalScheduleAsync(MedicalSchedule item);
        Task UpdateMedicalScheduleAsync(MedicalSchedule item);
        Task DeleteMedicalScheduleAsync(int id);


        Task<List<Attendance>> GetAttendanceAsync();
        Task CreateAttendanceAsync(Attendance item);

        Task<List<TrainingSession>> GetTrainingSessionsAsync();
        Task CreateTrainingSessionAsync(TrainingSession item);
        Task DeleteTrainingSessionAsync(int id);
    }
}
