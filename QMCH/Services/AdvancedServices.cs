using QMCH.Data;
using QMCH.Interfaces.Repositories;
using QMCH.Interfaces.Services;
using QMCH.Models;
using Microsoft.EntityFrameworkCore;

namespace QMCH.Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly ApplicationDbContext _context;

        public InsuranceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Insurance>> GetAllAsync()
        {
            return await _context.Insurances.AsNoTracking().ToListAsync();
        }

        public async Task<Insurance?> GetByIdAsync(int id)
        {
            return await _context.Insurances.FindAsync(id);
        }

        public async Task<List<Insurance>> GetByPatientIdAsync(int patientId)
        {
            return await _context.Insurances
                .Where(i => i.PatientId == patientId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Insurance> CreateAsync(Insurance insurance)
        {
            _context.Insurances.Add(insurance);
            await _context.SaveChangesAsync();
            return insurance;
        }

        public async Task<Insurance> UpdateAsync(Insurance insurance)
        {
            _context.Insurances.Update(insurance);
            await _context.SaveChangesAsync();
            return insurance;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var insurance = await _context.Insurances.FindAsync(id);
            if (insurance == null) return false;

            _context.Insurances.Remove(insurance);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    public class ClaimService : IClaimService
    {
        private readonly ApplicationDbContext _context;

        public ClaimService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Claim>> GetAllAsync()
        {
            return await _context.Claims.Include(c => c.Visit).AsNoTracking().ToListAsync();
        }

        public async Task<Claim?> GetByIdAsync(int id)
        {
            return await _context.Claims.Include(c => c.Visit).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Claim>> GetByPatientIdAsync(int patientId)
        {
            return await _context.Claims
                .Where(c => c.Visit.PatientId == patientId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Claim>> GetByStatusAsync(string status)
        {
            return await _context.Claims
                .Where(c => c.Status == status)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Claim> CreateAsync(Claim claim)
        {
            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();
            return claim;
        }

        public async Task<Claim> UpdateAsync(Claim claim)
        {
            _context.Claims.Update(claim);
            await _context.SaveChangesAsync();
            return claim;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var claim = await _context.Claims.FindAsync(id);
            if (claim == null) return false;

            _context.Claims.Remove(claim);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _context.Claims
                .Where(c => c.Status == "Paid")
                .SumAsync(c => c.AmountPaid ?? 0);
        }

        public async Task<decimal> GetOutstandingClaimsAsync()
        {
            return await _context.Claims
                .Where(c => c.Status == "Pending")
                .SumAsync(c => c.AmountBilled);
        }
    }

    public class ScheduleManagementService : IScheduleManagementService
    {
        private readonly ApplicationDbContext _context;

        public ScheduleManagementService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Schedule>> GetAllAsync()
        {
            return await _context.Schedules
                .Include(s => s.Staff)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Schedule?> GetByIdAsync(int id)
        {
            return await _context.Schedules.Include(s => s.Staff).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Schedule>> GetByStaffIdAsync(int staffId)
        {
            return await _context.Schedules
                .Where(s => s.StaffId == staffId && s.Status == "Active")
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Schedule>> GetByDateAsync(DateTime date)
        {
            return await _context.Schedules
                .Where(s => s.Date.Date == date.Date)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Schedule>> GetByStaffAndDateRangeAsync(int staffId, DateTime startDate, DateTime endDate)
        {
            return await _context.Schedules
                .Where(s => s.StaffId == staffId && s.Date >= startDate && s.Date <= endDate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Schedule> CreateAsync(Schedule schedule)
        {
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();
            return schedule;
        }

        public async Task<Schedule> UpdateAsync(Schedule schedule)
        {
            _context.Schedules.Update(schedule);
            await _context.SaveChangesAsync();
            return schedule;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null) return false;

            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> IsStaffAvailableAsync(int staffId, DateTime startTime, DateTime endTime)
        {
            var conflicts = await _context.Schedules
                .Where(s => s.StaffId == staffId &&
                            s.ShiftStartTime < endTime &&
                            s.ShiftEndTime > startTime &&
                            s.Status == "Active")
                .CountAsync();

            return conflicts == 0;
        }
    }

    public class TimeAttendanceService : ITimeAttendanceService
    {
        private readonly ApplicationDbContext _context;

        public TimeAttendanceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TimeAttendance>> GetAllAsync()
        {
            return await _context.TimeAttendances
                .Include(ta => ta.Staff)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TimeAttendance?> GetByIdAsync(int id)
        {
            return await _context.TimeAttendances.Include(ta => ta.Staff).FirstOrDefaultAsync(ta => ta.Id == id);
        }

        public async Task<List<TimeAttendance>> GetByStaffIdAsync(int staffId)
        {
            return await _context.TimeAttendances
                .Where(ta => ta.StaffId == staffId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<TimeAttendance>> GetByDateRangeAsync(int staffId, DateTime startDate, DateTime endDate)
        {
            return await _context.TimeAttendances
                .Where(ta => ta.StaffId == staffId && ta.ClockInTime.Date >= startDate.Date && ta.ClockInTime.Date <= endDate.Date)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TimeAttendance> ClockInAsync(int staffId, string? location, string? gpsCoordinates)
        {
            var attendance = new TimeAttendance
            {
                StaffId = staffId,
                ClockInTime = DateTime.Now,
                ClockInLocation = location,
                GPSCoordinates = gpsCoordinates,
                Status = "Active"
            };

            _context.TimeAttendances.Add(attendance);
            await _context.SaveChangesAsync();
            return attendance;
        }

        public async Task<TimeAttendance> ClockOutAsync(int id, string? location)
        {
            var attendance = await _context.TimeAttendances.FindAsync(id);
            if (attendance == null) throw new InvalidOperationException("Attendance record not found");

            attendance.ClockOutTime = DateTime.Now;
            attendance.ClockOutLocation = location;
            attendance.Status = "Completed";
            attendance.HoursWorked = (decimal)(attendance.ClockOutTime.Value - attendance.ClockInTime).TotalHours;

            _context.TimeAttendances.Update(attendance);
            await _context.SaveChangesAsync();
            return attendance;
        }

        public async Task<TimeAttendance> UpdateAsync(TimeAttendance attendance)
        {
            _context.TimeAttendances.Update(attendance);
            await _context.SaveChangesAsync();
            return attendance;
        }

        public async Task<decimal> CalculateHoursWorkedAsync(int staffId, DateTime startDate, DateTime endDate)
        {
            var records = await _context.TimeAttendances
                .Where(ta => ta.StaffId == staffId && 
                             ta.ClockInTime.Date >= startDate.Date && 
                             ta.ClockInTime.Date <= endDate.Date &&
                             ta.ClockOutTime.HasValue)
                .ToListAsync();

            return (decimal)records.Sum(ta => (ta.ClockOutTime.Value - ta.ClockInTime).TotalHours);
        }
    }

    public class TimeOffRequestService : ITimeOffRequestService
    {
        private readonly ApplicationDbContext _context;

        public TimeOffRequestService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TimeOffRequest>> GetAllAsync()
        {
            return await _context.TimeOffRequests
                .Include(tor => tor.Staff)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TimeOffRequest?> GetByIdAsync(int id)
        {
            return await _context.TimeOffRequests.Include(tor => tor.Staff).FirstOrDefaultAsync(tor => tor.Id == id);
        }

        public async Task<List<TimeOffRequest>> GetByStaffIdAsync(int staffId)
        {
            return await _context.TimeOffRequests
                .Where(tor => tor.StaffId == staffId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<TimeOffRequest>> GetPendingAsync()
        {
            return await _context.TimeOffRequests
                .Where(tor => tor.Status == "Pending")
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<TimeOffRequest> CreateAsync(TimeOffRequest request)
        {
            _context.TimeOffRequests.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<TimeOffRequest> ApproveAsync(int id, string approvedBy)
        {
            var request = await _context.TimeOffRequests.FindAsync(id);
            if (request == null) throw new InvalidOperationException("Request not found");

            request.Status = "Approved";
            request.ApprovedBy = approvedBy;
            _context.TimeOffRequests.Update(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<TimeOffRequest> DenyAsync(int id)
        {
            var request = await _context.TimeOffRequests.FindAsync(id);
            if (request == null) throw new InvalidOperationException("Request not found");

            request.Status = "Denied";
            _context.TimeOffRequests.Update(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var request = await _context.TimeOffRequests.FindAsync(id);
            if (request == null) return false;

            _context.TimeOffRequests.Remove(request);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    public class LicenseService : ILicenseService
    {
        private readonly ApplicationDbContext _context;

        public LicenseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<License>> GetAllAsync()
        {
            return await _context.Licenses
                .Include(l => l.Staff)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<License?> GetByIdAsync(int id)
        {
            return await _context.Licenses.Include(l => l.Staff).FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<List<License>> GetByStaffIdAsync(int staffId)
        {
            return await _context.Licenses
                .Where(l => l.StaffId == staffId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<License>> GetExpiringAsync(int daysThreshold = 30)
        {
            var thresholdDate = DateTime.Now.AddDays(daysThreshold);
            return await _context.Licenses
                .Where(l => l.ExpiryDate <= thresholdDate && l.ExpiryDate > DateTime.Now && l.Status == "Active")
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<License>> GetExpiredAsync()
        {
            return await _context.Licenses
                .Where(l => l.ExpiryDate < DateTime.Now)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<License> CreateAsync(License license)
        {
            _context.Licenses.Add(license);
            await _context.SaveChangesAsync();
            return license;
        }

        public async Task<License> UpdateAsync(License license)
        {
            _context.Licenses.Update(license);
            await _context.SaveChangesAsync();
            return license;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var license = await _context.Licenses.FindAsync(id);
            if (license == null) return false;

            _context.Licenses.Remove(license);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    public class ComplianceService : IComplianceService
    {
        private readonly ApplicationDbContext _context;

        public ComplianceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<StaffCompliance>> GetAllAsync()
        {
            return await _context.StaffCompliances
                .Include(sc => sc.Staff)
                .Include(sc => sc.ComplianceRequirement)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<StaffCompliance?> GetByIdAsync(int id)
        {
            return await _context.StaffCompliances
                .Include(sc => sc.Staff)
                .Include(sc => sc.ComplianceRequirement)
                .FirstOrDefaultAsync(sc => sc.Id == id);
        }

        public async Task<List<StaffCompliance>> GetByStaffIdAsync(int staffId)
        {
            return await _context.StaffCompliances
                .Where(sc => sc.StaffId == staffId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<StaffCompliance>> GetExpiredAsync()
        {
            return await _context.StaffCompliances
                .Where(sc => sc.ExpiryDate.HasValue && sc.ExpiryDate < DateTime.Now)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<StaffCompliance>> GetPendingAsync()
        {
            return await _context.StaffCompliances
                .Where(sc => sc.Status == "Pending")
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<StaffCompliance> CreateAsync(StaffCompliance compliance)
        {
            _context.StaffCompliances.Add(compliance);
            await _context.SaveChangesAsync();
            return compliance;
        }

        public async Task<StaffCompliance> UpdateAsync(StaffCompliance compliance)
        {
            _context.StaffCompliances.Update(compliance);
            await _context.SaveChangesAsync();
            return compliance;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var compliance = await _context.StaffCompliances.FindAsync(id);
            if (compliance == null) return false;

            _context.StaffCompliances.Remove(compliance);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Dictionary<int, int>> GetComplianceStatusByStaffAsync()
        {
            var result = new Dictionary<int, int>();
            var staff = await _context.StaffMembers.Select(s => s.Id).ToListAsync();
            
            foreach (var staffId in staff)
            {
                var count = await _context.StaffCompliances
                    .Where(sc => sc.StaffId == staffId && sc.Status == "Completed")
                    .CountAsync();
                result[staffId] = count;
            }

            return result;
        }
    }

    public class CarePlanService : ICarePlanService
    {
        private readonly ApplicationDbContext _context;

        public CarePlanService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CarePlan>> GetAllAsync()
        {
            return await _context.CarePlans
                .Include(cp => cp.Interventions_Collection)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<CarePlan?> GetByIdAsync(int id)
        {
            return await _context.CarePlans
                .Include(cp => cp.Interventions_Collection)
                .FirstOrDefaultAsync(cp => cp.Id == id);
        }

        public async Task<List<CarePlan>> GetByPatientIdAsync(int patientId)
        {
            return await _context.CarePlans
                .Where(cp => cp.PatientId == patientId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<CarePlan>> GetActiveAsync()
        {
            return await _context.CarePlans
                .Where(cp => cp.Status == "Active")
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<CarePlan> CreateAsync(CarePlan carePlan)
        {
            _context.CarePlans.Add(carePlan);
            await _context.SaveChangesAsync();
            return carePlan;
        }

        public async Task<CarePlan> UpdateAsync(CarePlan carePlan)
        {
            _context.CarePlans.Update(carePlan);
            await _context.SaveChangesAsync();
            return carePlan;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var carePlan = await _context.CarePlans.FindAsync(id);
            if (carePlan == null) return false;

            _context.CarePlans.Remove(carePlan);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    public class AssessmentService : IAssessmentService
    {
        private readonly ApplicationDbContext _context;

        public AssessmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PatientAssessment>> GetAllAsync()
        {
            return await _context.PatientAssessments.AsNoTracking().ToListAsync();
        }

        public async Task<PatientAssessment?> GetByIdAsync(int id)
        {
            return await _context.PatientAssessments.FindAsync(id);
        }

        public async Task<List<PatientAssessment>> GetByPatientIdAsync(int patientId)
        {
            return await _context.PatientAssessments
                .Where(pa => pa.PatientId == patientId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<PatientAssessment?> GetLatestAssessmentAsync(int patientId)
        {
            return await _context.PatientAssessments
                .Where(pa => pa.PatientId == patientId)
                .OrderByDescending(pa => pa.AssessmentDate)
                .FirstOrDefaultAsync();
        }

        public async Task<PatientAssessment> CreateAsync(PatientAssessment assessment)
        {
            _context.PatientAssessments.Add(assessment);
            await _context.SaveChangesAsync();
            return assessment;
        }

        public async Task<PatientAssessment> UpdateAsync(PatientAssessment assessment)
        {
            _context.PatientAssessments.Update(assessment);
            await _context.SaveChangesAsync();
            return assessment;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var assessment = await _context.PatientAssessments.FindAsync(id);
            if (assessment == null) return false;

            _context.PatientAssessments.Remove(assessment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
