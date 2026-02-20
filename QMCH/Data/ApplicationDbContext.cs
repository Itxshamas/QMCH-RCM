using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QMCH.Models;

namespace QMCH.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Core Quality Homecare Modules (New Architecture)
        public DbSet<Patient> Patients { get; set; } = null!;
        public DbSet<Staff> StaffMembers { get; set; } = null!;
        public DbSet<Skill> Skills { get; set; } = null!;
        public DbSet<StaffSkill> StaffSkills { get; set; } = null!;
        public DbSet<Visit> Visits { get; set; } = null!;
        public DbSet<VisitDocumentation> VisitDocumentations { get; set; } = null!;
        public DbSet<Insurance> Insurances { get; set; } = null!;
        public DbSet<Claim> Claims { get; set; } = null!;
        public DbSet<Medication> Medications { get; set; } = null!;
        public DbSet<AuditLog> AuditLogs { get; set; } = null!;
        public DbSet<Schedule> Schedules { get; set; } = null!;
        public DbSet<TimeAttendance> TimeAttendances { get; set; } = null!;
        public DbSet<TimeOffRequest> TimeOffRequests { get; set; } = null!;
        public DbSet<License> Licenses { get; set; } = null!;
        public DbSet<ComplianceRequirement> ComplianceRequirements { get; set; } = null!;
        public DbSet<StaffCompliance> StaffCompliances { get; set; } = null!;
        public DbSet<QualityAssurance> QualityAssurances { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;
        public DbSet<DocumentRecord> DocumentRecords { get; set; } = null!;
        public DbSet<CarePlan> CarePlans { get; set; } = null!;
        public DbSet<CarePlanIntervention> CarePlanInterventions { get; set; } = null!;
        public DbSet<PatientAssessment> PatientAssessments { get; set; } = null!;

        // Legacy DbSets (Keep for backward compatibility)
        [Obsolete] public DbSet<Client> Clients { get; set; } = null!;
        [Obsolete] public DbSet<Employee> Employees { get; set; } = null!;
        [Obsolete] public DbSet<ClientType> ClientTypes { get; set; } = null!;
        [Obsolete] public DbSet<ServiceType> ServiceTypes { get; set; } = null!;
        [Obsolete] public DbSet<Location> Locations { get; set; } = null!;
        [Obsolete] public DbSet<Classification> Classifications { get; set; } = null!;
        [Obsolete] public DbSet<MedicalSchedule> MedicalSchedules { get; set; } = null!;
        [Obsolete] public DbSet<Attendance> Attendances { get; set; } = null!;
        [Obsolete] public DbSet<TrainingSession> TrainingSessions { get; set; } = null!;
        [Obsolete] public DbSet<JobOrder> JobOrders { get; set; } = null!;
        [Obsolete] public DbSet<HRAgency> HRAgencies { get; set; } = null!;
        [Obsolete] public DbSet<Applicant> Applicants { get; set; } = null!;
        [Obsolete] public DbSet<Interview> Interviews { get; set; } = null!;
        [Obsolete] public DbSet<Resume> Resumes { get; set; } = null!;
        [Obsolete] public DbSet<ScheduleItem> ScheduleItems { get; set; } = null!;
        [Obsolete] public DbSet<TimesheetPayroll> TimesheetPayrolls { get; set; } = null!;
        [Obsolete] public DbSet<TimesheetBilling> TimesheetBillings { get; set; } = null!;
        [Obsolete] public DbSet<ServiceOrderSummary> ServiceOrderSummaries { get; set; } = null!;
        [Obsolete] public DbSet<Leave> Leaves { get; set; } = null!;
        [Obsolete] public DbSet<Report> Reports { get; set; } = null!;
        [Obsolete] public DbSet<AppRole> AppRoles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Staff - Skill (Many-to-Many)
            builder.Entity<StaffSkill>()
                .HasKey(ss => new { ss.StaffId, ss.SkillId });

            builder.Entity<StaffSkill>()
                .HasOne(ss => ss.Staff)
                .WithMany(s => s.StaffSkills)
                .HasForeignKey(ss => ss.StaffId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<StaffSkill>()
                .HasOne(ss => ss.Skill)
                .WithMany(s => s.StaffSkills)
                .HasForeignKey(ss => ss.SkillId)
                .OnDelete(DeleteBehavior.Cascade);

            // Patient - Visit (One-to-Many)
            builder.Entity<Visit>()
                .HasOne(v => v.Patient)
                .WithMany(p => p.Visits)
                .HasForeignKey(v => v.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Staff - Visit (One-to-Many)
            builder.Entity<Visit>()
                .HasOne(v => v.Staff)
                .WithMany(s => s.Visits)
                .HasForeignKey(v => v.StaffId)
                .OnDelete(DeleteBehavior.SetNull);

            // Visit - Documentation (One-to-Many)
            builder.Entity<VisitDocumentation>()
                .HasOne(vd => vd.Visit)
                .WithMany(v => v.Documentations)
                .HasForeignKey(vd => vd.VisitId)
                .OnDelete(DeleteBehavior.Cascade);

            // Patient - Insurance (One-to-Many)
            builder.Entity<Insurance>()
                .HasOne(i => i.Patient)
                .WithMany(p => p.Insurances)
                .HasForeignKey(i => i.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Patient - Medication (One-to-Many)
            builder.Entity<Medication>()
                .HasOne(m => m.Patient)
                .WithMany(p => p.Medications)
                .HasForeignKey(m => m.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Patient - CarePlan (One-to-Many)
            builder.Entity<CarePlan>()
                .HasOne(cp => cp.Patient)
                .WithMany()
                .HasForeignKey(cp => cp.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // CarePlan - CarePlanIntervention (One-to-Many)
            builder.Entity<CarePlanIntervention>()
                .HasOne(cpi => cpi.CarePlan)
                .WithMany(cp => cp.Interventions_Collection)
                .HasForeignKey(cpi => cpi.CarePlanId)
                .OnDelete(DeleteBehavior.Cascade);

            // Patient - PatientAssessment (One-to-Many)
            builder.Entity<PatientAssessment>()
                .HasOne(pa => pa.Patient)
                .WithMany()
                .HasForeignKey(pa => pa.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Staff - License (One-to-Many)
            builder.Entity<License>()
                .HasOne(l => l.Staff)
                .WithMany()
                .HasForeignKey(l => l.StaffId)
                .OnDelete(DeleteBehavior.Cascade);

            // Staff - StaffCompliance (One-to-Many)
            builder.Entity<StaffCompliance>()
                .HasOne(sc => sc.Staff)
                .WithMany()
                .HasForeignKey(sc => sc.StaffId)
                .OnDelete(DeleteBehavior.Cascade);

            // ComplianceRequirement - StaffCompliance (One-to-Many)
            builder.Entity<StaffCompliance>()
                .HasOne(sc => sc.ComplianceRequirement)
                .WithMany()
                .HasForeignKey(sc => sc.ComplianceRequirementId);

            // Staff - Schedule (One-to-Many)
            builder.Entity<Schedule>()
                .HasOne(s => s.Staff)
                .WithMany()
                .HasForeignKey(s => s.StaffId)
                .OnDelete(DeleteBehavior.Cascade);

            // Staff - TimeAttendance (One-to-Many)
            builder.Entity<TimeAttendance>()
                .HasOne(ta => ta.Staff)
                .WithMany()
                .HasForeignKey(ta => ta.StaffId)
                .OnDelete(DeleteBehavior.Cascade);

            // Staff - TimeOffRequest (One-to-Many)
            builder.Entity<TimeOffRequest>()
                .HasOne(tor => tor.Staff)
                .WithMany()
                .HasForeignKey(tor => tor.StaffId)
                .OnDelete(DeleteBehavior.Cascade);

            // User - Message (One-to-Many)
            builder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.Entity<Message>()
                .HasOne(m => m.Recipient)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.RecipientId)
                .OnDelete(DeleteBehavior.Restrict); 

            // User - Notification (One-to-Many)
            builder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User - DocumentRecord (One-to-Many)
            builder.Entity<DocumentRecord>()
                .HasOne(dr => dr.UploadedByUser)
                .WithMany(u => u.DocumentRecords)
                .HasForeignKey(dr => dr.UploadedBy)
                .OnDelete(DeleteBehavior.SetNull);

            // User - AuditLog (One-to-Many)
            builder.Entity<AuditLog>()
                .HasOne(a => a.User)
                .WithMany(u => u.AuditLogs)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            // Decimal Precision
            builder.Entity<Claim>()
                .Property(c => c.AmountBilled)
                .HasPrecision(18, 2);

            builder.Entity<Claim>()
                .Property(c => c.AmountPaid)
                .HasPrecision(18, 2);
        }
    }
}
