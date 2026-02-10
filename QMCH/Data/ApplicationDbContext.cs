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

        // Client Module
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientType> ClientTypes { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<Location> Locations { get; set; }

        // Employee Module
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Classification> Classifications { get; set; }
        public DbSet<MedicalSchedule> MedicalSchedules { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<TrainingSession> TrainingSessions { get; set; }

        // HR / Recruitment Module
        public DbSet<JobOrder> JobOrders { get; set; }
        public DbSet<HRAgency> HRAgencies { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Interview> Interviews { get; set; }

        // Operations Module
        public DbSet<ScheduleItem> ScheduleItems { get; set; }
        public DbSet<TimesheetPayroll> TimesheetPayrolls { get; set; }
        public DbSet<TimesheetBilling> TimesheetBillings { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // No relationships configured - flat table design
            // No navigation properties
            // No FK constraints

            // Ignore computed properties so EF doesn't try to map them
            builder.Entity<TimesheetPayroll>().Ignore(t => t.TotalHours);
            builder.Entity<TimesheetPayroll>().Ignore(t => t.TotalAmount);
            builder.Entity<TimesheetBilling>().Ignore(t => t.TotalAmount);
            builder.Entity<Leave>().Ignore(t => t.TotalDays);
            builder.Entity<User>().Ignore(u => u.FullName);
        }
    }
}
