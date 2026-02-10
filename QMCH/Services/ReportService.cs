using Microsoft.EntityFrameworkCore;
using QMCH.Data;
using QMCH.Models;

namespace QMCH.Services
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _db;
        public ReportService(ApplicationDbContext db) { _db = db; }

        public async Task<List<Report>> GetHRReportsAsync()
        {
            return await _db.Reports
                .Where(r => r.Category == "HR")
                .OrderByDescending(r => r.GeneratedDate)
                .ToListAsync();
        }

        public async Task<List<Report>> GetOperationReportsAsync()
        {
            return await _db.Reports
                .Where(r => r.Category == "Operation")
                .OrderByDescending(r => r.GeneratedDate)
                .ToListAsync();
        }
    }
}
