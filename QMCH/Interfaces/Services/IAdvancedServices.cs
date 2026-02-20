using QMCH.Models;

namespace QMCH.Interfaces.Services
{
    public interface IInsuranceService
    {
        Task<List<Insurance>> GetAllAsync();
        Task<Insurance?> GetByIdAsync(int id);
        Task<List<Insurance>> GetByPatientIdAsync(int patientId);
        Task<Insurance> CreateAsync(Insurance insurance);
        Task<Insurance> UpdateAsync(Insurance insurance);
        Task<bool> DeleteAsync(int id);
    }

    public interface IClaimService
    {
        Task<List<Claim>> GetAllAsync();
        Task<Claim?> GetByIdAsync(int id);
        Task<List<Claim>> GetByPatientIdAsync(int patientId);
        Task<List<Claim>> GetByStatusAsync(string status);
        Task<Claim> CreateAsync(Claim claim);
        Task<Claim> UpdateAsync(Claim claim);
        Task<bool> DeleteAsync(int id);
        Task<decimal> GetTotalRevenueAsync();
        Task<decimal> GetOutstandingClaimsAsync();
    }

    public interface IScheduleManagementService
    {
        Task<List<Schedule>> GetAllAsync();
        Task<Schedule?> GetByIdAsync(int id);
        Task<List<Schedule>> GetByStaffIdAsync(int staffId);
        Task<List<Schedule>> GetByDateAsync(DateTime date);
        Task<List<Schedule>> GetByStaffAndDateRangeAsync(int staffId, DateTime startDate, DateTime endDate);
        Task<Schedule> CreateAsync(Schedule schedule);
        Task<Schedule> UpdateAsync(Schedule schedule);
        Task<bool> DeleteAsync(int id);
        Task<bool> IsStaffAvailableAsync(int staffId, DateTime startTime, DateTime endTime);
    }

    public interface ITimeAttendanceService
    {
        Task<List<TimeAttendance>> GetAllAsync();
        Task<TimeAttendance?> GetByIdAsync(int id);
        Task<List<TimeAttendance>> GetByStaffIdAsync(int staffId);
        Task<List<TimeAttendance>> GetByDateRangeAsync(int staffId, DateTime startDate, DateTime endDate);
        Task<TimeAttendance> ClockInAsync(int staffId, string? location, string? gpsCoordinates);
        Task<TimeAttendance> ClockOutAsync(int id, string? location);
        Task<TimeAttendance> UpdateAsync(TimeAttendance attendance);
        Task<decimal> CalculateHoursWorkedAsync(int staffId, DateTime startDate, DateTime endDate);
    }

    public interface ITimeOffRequestService
    {
        Task<List<TimeOffRequest>> GetAllAsync();
        Task<TimeOffRequest?> GetByIdAsync(int id);
        Task<List<TimeOffRequest>> GetByStaffIdAsync(int staffId);
        Task<List<TimeOffRequest>> GetPendingAsync();
        Task<TimeOffRequest> CreateAsync(TimeOffRequest request);
        Task<TimeOffRequest> ApproveAsync(int id, string approvedBy);
        Task<TimeOffRequest> DenyAsync(int id);
        Task<bool> DeleteAsync(int id);
    }

    public interface ILicenseService
    {
        Task<List<License>> GetAllAsync();
        Task<License?> GetByIdAsync(int id);
        Task<List<License>> GetByStaffIdAsync(int staffId);
        Task<List<License>> GetExpiringAsync(int daysThreshold = 30);
        Task<List<License>> GetExpiredAsync();
        Task<License> CreateAsync(License license);
        Task<License> UpdateAsync(License license);
        Task<bool> DeleteAsync(int id);
    }

    public interface IComplianceService
    {
        Task<List<StaffCompliance>> GetAllAsync();
        Task<StaffCompliance?> GetByIdAsync(int id);
        Task<List<StaffCompliance>> GetByStaffIdAsync(int staffId);
        Task<List<StaffCompliance>> GetExpiredAsync();
        Task<List<StaffCompliance>> GetPendingAsync();
        Task<StaffCompliance> CreateAsync(StaffCompliance compliance);
        Task<StaffCompliance> UpdateAsync(StaffCompliance compliance);
        Task<bool> DeleteAsync(int id);
        Task<Dictionary<int, int>> GetComplianceStatusByStaffAsync();
    }

    public interface ICarePlanService
    {
        Task<List<CarePlan>> GetAllAsync();
        Task<CarePlan?> GetByIdAsync(int id);
        Task<List<CarePlan>> GetByPatientIdAsync(int patientId);
        Task<List<CarePlan>> GetActiveAsync();
        Task<CarePlan> CreateAsync(CarePlan carePlan);
        Task<CarePlan> UpdateAsync(CarePlan carePlan);
        Task<bool> DeleteAsync(int id);
    }

    public interface IAssessmentService
    {
        Task<List<PatientAssessment>> GetAllAsync();
        Task<PatientAssessment?> GetByIdAsync(int id);
        Task<List<PatientAssessment>> GetByPatientIdAsync(int patientId);
        Task<PatientAssessment?> GetLatestAssessmentAsync(int patientId);
        Task<PatientAssessment> CreateAsync(PatientAssessment assessment);
        Task<PatientAssessment> UpdateAsync(PatientAssessment assessment);
        Task<bool> DeleteAsync(int id);
    }

    public interface IReportingService
    {
        // Operational Reports
        Task<object> GetDailyCensusAsync(DateTime date);
        Task<object> GetStaffProductivityAsync(DateTime startDate, DateTime endDate);
        Task<object> GetVisitCompletionRateAsync(DateTime startDate, DateTime endDate);
        Task<object> GetMissedVisitsAsync(DateTime startDate, DateTime endDate);

        // Financial Reports
        Task<object> GetRevenueReportAsync(DateTime startDate, DateTime endDate);
        Task<object> GetAccountsReceivableAsync();
        Task<object> GetPayerAnalysisAsync();

        // Clinical Reports
        Task<object> GetPatientOutcomesAsync(DateTime startDate, DateTime endDate);
        Task<object> GetCarePlanComplianceAsync();
        Task<object> GetIncidentReportsAsync(DateTime startDate, DateTime endDate);

        // Compliance Reports
        Task<object> GetLicenseExpiryReportAsync();
        Task<object> GetTrainingCompletionAsync();
        Task<object> GetQualityMetricsAsync();
    }

    public interface ICommunicationService
    {
        Task<List<Message>> GetInboxAsync(string userId);
        Task<List<Message>> GetSentAsync(string userId);
        Task<Message> SendMessageAsync(string senderId, string recipientId, string subject, string content, string priority = "Normal");
        Task<Message> MarkAsReadAsync(int messageId);
        Task<bool> DeleteMessageAsync(int messageId);

        Task<List<Notification>> GetNotificationsAsync(string userId);
        Task<Notification> CreateNotificationAsync(string userId, string title, string content, string type = "Info", string? actionLink = null);
        Task<Notification> MarkNotificationAsReadAsync(int notificationId);
        Task<bool> DeleteNotificationAsync(int notificationId);
    }

    public interface IDocumentService
    {
        Task<List<DocumentRecord>> GetAllAsync();
        Task<DocumentRecord?> GetByIdAsync(int id);
        Task<List<DocumentRecord>> GetByEntityAsync(string entityType, int entityId);
        Task<DocumentRecord> UploadAsync(DocumentRecord document, Stream fileStream);
        Task<byte[]> DownloadAsync(int documentId);
        Task<bool> DeleteAsync(int id);
    }

    public interface IAuditService
    {
        Task<List<AuditLog>> GetAllAsync();
        Task<List<AuditLog>> GetByUserIdAsync(string userId);
        Task<List<AuditLog>> GetByEntityAsync(string entityName, string entityId);
        Task LogActionAsync(string? userId, string action, string entityName, string? entityId, string? oldValues, string? newValues, string? ipAddress);
        Task<List<AuditLog>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
