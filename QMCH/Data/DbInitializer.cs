using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QMCH.Models;

namespace QMCH.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.MigrateAsync();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            // Seed Roles
            string[] roleNames = { "Admin", "Scheduler", "Caregiver", "Billing Staff", "Supervisor", "HR Manager", "Nurse" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Seed Admin User
            var adminEmail = "admin@qhmcpro.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "System",
                    LastName = "Administrator",
                    Department = "IT",
                    JobTitle = "System Admin",
                    EmailConfirmed = true,
                    IsActive = true
                };
                await userManager.CreateAsync(adminUser, "Admin@12345");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            // Seed Client Types
            if (!await context.ClientTypes.AnyAsync())
            {
                context.ClientTypes.AddRange(
                    new ClientType { ShortDescription = "Home Health", Description = "Home health care patients" },
                    new ClientType { ShortDescription = "Hospice", Description = "Hospice care patients" },
                    new ClientType { ShortDescription = "Private Duty", Description = "Private duty care clients" },
                    new ClientType { ShortDescription = "Skilled Nursing", Description = "Skilled nursing facility clients" }
                );
                await context.SaveChangesAsync();
            }

            // Seed Service Types
            if (!await context.ServiceTypes.AnyAsync())
            {
                context.ServiceTypes.AddRange(
                    new ServiceType { Description = "Personal Care", Code = "PC", DefaultRate = 25.00m },
                    new ServiceType { Description = "Skilled Nursing", Code = "SN", DefaultRate = 65.00m },
                    new ServiceType { Description = "Physical Therapy", Code = "PT", DefaultRate = 75.00m },
                    new ServiceType { Description = "Occupational Therapy", Code = "OT", DefaultRate = 70.00m },
                    new ServiceType { Description = "Speech Therapy", Code = "ST", DefaultRate = 70.00m },
                    new ServiceType { Description = "Home Health Aide", Code = "HHA", DefaultRate = 22.00m },
                    new ServiceType { Description = "Companionship", Code = "COMP", DefaultRate = 18.00m }
                );
                await context.SaveChangesAsync();
            }

            // Seed Classifications
            if (!await context.Classifications.AnyAsync())
            {
                context.Classifications.AddRange(
                    new Classification { Name = "Registered Nurse (RN)", Code = "RN" },
                    new Classification { Name = "Licensed Practical Nurse (LPN)", Code = "LPN" },
                    new Classification { Name = "Certified Nursing Assistant (CNA)", Code = "CNA" },
                    new Classification { Name = "Home Health Aide (HHA)", Code = "HHA" },
                    new Classification { Name = "Physical Therapist (PT)", Code = "PT" },
                    new Classification { Name = "Occupational Therapist (OT)", Code = "OT" },
                    new Classification { Name = "Speech Therapist (ST)", Code = "ST" }
                );
                await context.SaveChangesAsync();
            }

            // Seed App Roles
            if (!await context.AppRoles.AnyAsync())
            {
                context.AppRoles.AddRange(
                    new AppRole { Name = "Administrator", Description = "Full system access", IsSystem = true },
                    new AppRole { Name = "Scheduler", Description = "Manage schedules and assignments", IsSystem = true },
                    new AppRole { Name = "Billing Specialist", Description = "Manage billing and invoicing", IsSystem = true },
                    new AppRole { Name = "HR Manager", Description = "Manage recruitment and employees", IsSystem = true },
                    new AppRole { Name = "Supervisor", Description = "Oversee operations and approve timesheets", IsSystem = true }
                );
                await context.SaveChangesAsync();
            }

            // Seed Reports
            if (!await context.Reports.AnyAsync())
            {
                context.Reports.AddRange(
                    new Report { Title = "Employee Roster", Category = "HR", Description = "List of all active employees", IsSystem = true },
                    new Report { Title = "Open Positions", Category = "HR", Description = "Current job openings and requisitions", IsSystem = true },
                    new Report { Title = "Applicant Pipeline", Category = "HR", Description = "Recruitment funnel analytics", IsSystem = true },
                    new Report { Title = "Client Census", Category = "Operations", Description = "Active client listing and demographics", IsSystem = true },
                    new Report { Title = "Payroll Summary", Category = "Operations", Description = "Weekly payroll totals and breakdowns", IsSystem = true },
                    new Report { Title = "Billing Summary", Category = "Operations", Description = "Billing totals and outstanding invoices", IsSystem = true },
                    new Report { Title = "Schedule Utilization", Category = "Operations", Description = "Staff scheduling and utilization metrics", IsSystem = true },
                    new Report { Title = "Attendance Report", Category = "Operations", Description = "Employee attendance and tardiness tracking", IsSystem = true }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
