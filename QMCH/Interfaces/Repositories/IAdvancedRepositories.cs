using QMCH.Models;

namespace QMCH.Interfaces.Repositories
{
    public interface IInsuranceRepository : IGenericRepository<Insurance>
    {
        Task<List<Insurance>> GetByPatientIdAsync(int patientId);
        Task<Insurance?> GetPrimaryInsuranceAsync(int patientId);
    }

    public interface IClaimRepository : IGenericRepository<Claim>
    {
        Task<List<Claim>> GetByStatusAsync(string status);
        Task<List<Claim>> GetByPatientIdAsync(int patientId);
        Task<decimal> GetTotalBilledAsync();
        Task<decimal> GetTotalPaidAsync();
        Task<List<Claim>> GetOutstandingClaimsAsync();
    }

    public interface IScheduleRepository : IGenericRepository<Schedule>
    {
        Task<List<Schedule>> GetByStaffIdAsync(int staffId);
        Task<List<Schedule>> GetByDateAsync(DateTime date);
        Task<List<Schedule>> GetByStaffAndDateRangeAsync(int staffId, DateTime startDate, DateTime endDate);
        Task<bool> IsTimeSlotAvailableAsync(int staffId, DateTime startTime, DateTime endTime);
    }

    public interface ITimeAttendanceRepository : IGenericRepository<TimeAttendance>
    {
        Task<List<TimeAttendance>> GetByStaffIdAsync(int staffId);
        Task<List<TimeAttendance>> GetByDateRangeAsync(int staffId, DateTime startDate, DateTime endDate);
        Task<TimeAttendance?> GetActiveClockinAsync(int staffId);
        Task<decimal> CalculateHoursWorkedAsync(int staffId, DateTime startDate, DateTime endDate);
    }

    public interface ITimeOffRequestRepository : IGenericRepository<TimeOffRequest>
    {
        Task<List<TimeOffRequest>> GetByStaffIdAsync(int staffId);
        Task<List<TimeOffRequest>> GetPendingAsync();
        Task<List<TimeOffRequest>> GetApprovedAsync(int staffId, DateTime startDate, DateTime endDate);
    }

    public interface ILicenseRepository : IGenericRepository<License>
    {
        Task<List<License>> GetByStaffIdAsync(int staffId);
        Task<List<License>> GetExpiringAsync(int daysThreshold);
        Task<List<License>> GetExpiredAsync();
    }

    public interface IComplianceRepository : IGenericRepository<StaffCompliance>
    {
        Task<List<StaffCompliance>> GetByStaffIdAsync(int staffId);
        Task<List<StaffCompliance>> GetExpiredAsync();
        Task<List<StaffCompliance>> GetPendingAsync();
        Task<Dictionary<int, int>> GetComplianceCountByStaffAsync();
    }

    public interface ICarePlanRepository : IGenericRepository<CarePlan>
    {
        Task<List<CarePlan>> GetByPatientIdAsync(int patientId);
        Task<List<CarePlan>> GetActiveAsync();
        Task<CarePlan?> GetLatestAsync(int patientId);
    }

    public interface IAssessmentRepository : IGenericRepository<PatientAssessment>
    {
        Task<List<PatientAssessment>> GetByPatientIdAsync(int patientId);
        Task<PatientAssessment?> GetLatestAsync(int patientId);
        Task<List<PatientAssessment>> GetByTypeAsync(string assessmentType);
    }

    public interface IMessageRepository : IGenericRepository<Message>
    {
        Task<List<Message>> GetInboxAsync(string userId);
        Task<List<Message>> GetSentAsync(string userId);
        Task<List<Message>> GetUnreadAsync(string userId);
        Task<int> GetUnreadCountAsync(string userId);
    }

    public interface INotificationRepository : IGenericRepository<Notification>
    {
        Task<List<Notification>> GetByUserIdAsync(string userId);
        Task<List<Notification>> GetUnreadAsync(string userId);
        Task<int> GetUnreadCountAsync(string userId);
    }

    public interface IDocumentRecordRepository : IGenericRepository<DocumentRecord>
    {
        Task<List<DocumentRecord>> GetByEntityAsync(string entityType, int? entityId);
        Task<List<DocumentRecord>> GetByTypeAsync(string documentType);
    }

    public interface IAuditLogRepository : IGenericRepository<AuditLog>
    {
        Task<List<AuditLog>> GetByUserIdAsync(string userId);
        Task<List<AuditLog>> GetByEntityAsync(string entityName, string entityId);
        Task<List<AuditLog>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<List<AuditLog>> GetByActionAsync(string action);
    }
}
