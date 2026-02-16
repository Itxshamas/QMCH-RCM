using QMCH.Data;
using QMCH.Interfaces.Services;
using QMCH.Models;
using Microsoft.EntityFrameworkCore;

namespace QMCH.Services
{
    public class CommunicationService : ICommunicationService
    {
        private readonly ApplicationDbContext _context;

        public CommunicationService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Message Management
        public async Task<List<Message>> GetInboxAsync(string userId)
        {
            return await _context.Messages
                .Where(m => m.RecipientId == userId)
                .OrderByDescending(m => m.SentAt)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Message>> GetSentAsync(string userId)
        {
            return await _context.Messages
                .Where(m => m.SenderId == userId)
                .OrderByDescending(m => m.SentAt)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Message> SendMessageAsync(string senderId, string recipientId, string subject, string content, string priority = "Normal")
        {
            var message = new Message
            {
                SenderId = senderId,
                RecipientId = recipientId,
                Subject = subject,
                Content = content,
                Priority = priority,
                SentAt = DateTime.Now,
                IsRead = false
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return message;
        }

        public async Task<Message> MarkAsReadAsync(int messageId)
        {
            var message = await _context.Messages.FindAsync(messageId);
            if (message == null) throw new InvalidOperationException("Message not found");

            message.IsRead = true;
            message.ReadAt = DateTime.Now;
            _context.Messages.Update(message);
            await _context.SaveChangesAsync();

            return message;
        }

        public async Task<bool> DeleteMessageAsync(int messageId)
        {
            var message = await _context.Messages.FindAsync(messageId);
            if (message == null) return false;

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return true;
        }

        // Notification Management
        public async Task<List<Notification>> GetNotificationsAsync(string userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Notification> CreateNotificationAsync(string userId, string title, string content, string type = "Info", string? actionLink = null)
        {
            var notification = new Notification
            {
                UserId = userId,
                Title = title,
                Content = content,
                Type = type,
                ActionLink = actionLink,
                CreatedAt = DateTime.Now,
                IsRead = false
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return notification;
        }

        public async Task<Notification> MarkNotificationAsReadAsync(int notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification == null) throw new InvalidOperationException("Notification not found");

            notification.IsRead = true;
            notification.ReadAt = DateTime.Now;
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync();

            return notification;
        }

        public async Task<bool> DeleteNotificationAsync(int notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification == null) return false;

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    public class DocumentService : IDocumentService
    {
        private readonly ApplicationDbContext _context;
        private readonly string _documentPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "documents");

        public DocumentService(ApplicationDbContext context)
        {
            _context = context;
            if (!Directory.Exists(_documentPath))
                Directory.CreateDirectory(_documentPath);
        }

        public async Task<List<DocumentRecord>> GetAllAsync()
        {
            return await _context.DocumentRecords
                .Include(dr => dr.UploadedByUser)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<DocumentRecord?> GetByIdAsync(int id)
        {
            return await _context.DocumentRecords
                .Include(dr => dr.UploadedByUser)
                .FirstOrDefaultAsync(dr => dr.Id == id);
        }

        public async Task<List<DocumentRecord>> GetByEntityAsync(string entityType, int entityId)
        {
            return await _context.DocumentRecords
                .Where(dr => dr.EntityType == entityType && dr.EntityId == entityId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<DocumentRecord> UploadAsync(DocumentRecord document, Stream fileStream)
        {
            try
            {
                // Generate unique file name
                var fileName = $"{Guid.NewGuid()}_{document.FileName}";
                var filePath = Path.Combine(_documentPath, fileName);

                // Save file
                using (var file = System.IO.File.Create(filePath))
                {
                    await fileStream.CopyToAsync(file);
                }

                document.FilePath = $"/uploads/documents/{fileName}";
                document.FileSize = new FileInfo(filePath).Length;
                document.FileExtension = Path.GetExtension(document.FileName);
                document.Status = "Active";
                document.CreatedAt = DateTime.Now;

                _context.DocumentRecords.Add(document);
                await _context.SaveChangesAsync();

                return document;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error uploading document: {ex.Message}", ex);
            }
        }

        public async Task<byte[]> DownloadAsync(int documentId)
        {
            var document = await _context.DocumentRecords.FindAsync(documentId);
            if (document == null) throw new InvalidOperationException("Document not found");

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", document.FilePath.TrimStart('/'));
            if (!System.IO.File.Exists(filePath)) throw new InvalidOperationException("File not found");

            return await System.IO.File.ReadAllBytesAsync(filePath);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var document = await _context.DocumentRecords.FindAsync(id);
            if (document == null) return false;

            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", document.FilePath.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                _context.DocumentRecords.Remove(document);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public class AuditService : IAuditService
    {
        private readonly ApplicationDbContext _context;

        public AuditService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AuditLog>> GetAllAsync()
        {
            return await _context.AuditLogs
                .Include(a => a.User)
                .OrderByDescending(a => a.Timestamp)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<AuditLog>> GetByUserIdAsync(string userId)
        {
            return await _context.AuditLogs
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.Timestamp)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<AuditLog>> GetByEntityAsync(string entityName, string entityId)
        {
            return await _context.AuditLogs
                .Where(a => a.EntityName == entityName && a.EntityId == entityId)
                .OrderByDescending(a => a.Timestamp)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task LogActionAsync(string? userId, string action, string entityName, string? entityId, string? oldValues, string? newValues, string? ipAddress)
        {
            var auditLog = new AuditLog
            {
                UserId = userId,
                Action = action,
                EntityName = entityName,
                EntityId = entityId,
                OldValues = oldValues,
                NewValues = newValues,
                Timestamp = DateTime.Now,
                IPAddress = ipAddress
            };

            _context.AuditLogs.Add(auditLog);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AuditLog>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.AuditLogs
                .Where(a => a.Timestamp >= startDate && a.Timestamp <= endDate)
                .OrderByDescending(a => a.Timestamp)
                .AsNoTracking()
                .ToListAsync();
        }
    }

    public class ReportingService : IReportingService
    {
        private readonly ApplicationDbContext _context;

        public ReportingService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Operational Reports
        public async Task<object> GetDailyCensusAsync(DateTime date)
        {
            var activePatients = await _context.Patients
                .Where(p => p.Status == "Active" && p.AdmissionDate <= date)
                .CountAsync();

            var visitCount = await _context.Visits
                .Where(v => v.ScheduledStartTime.Date == date.Date)
                .CountAsync();

            return new
            {
                Date = date.Date,
                ActivePatientCount = activePatients,
                ScheduledVisits = visitCount
            };
        }

        public async Task<object> GetStaffProductivityAsync(DateTime startDate, DateTime endDate)
        {
            var staffProductivity = await _context.Visits
                .Where(v => v.ScheduledStartTime >= startDate && v.ScheduledStartTime <= endDate)
                .GroupBy(v => v.StaffId)
                .Select(g => new
                {
                    StaffId = g.Key,
                    VisitCount = g.Count(),
                    CompletedVisits = g.Count(v => v.Status == "Completed")
                })
                .ToListAsync();

            return staffProductivity;
        }

        public async Task<object> GetVisitCompletionRateAsync(DateTime startDate, DateTime endDate)
        {
            var totalVisits = await _context.Visits
                .Where(v => v.ScheduledStartTime >= startDate && v.ScheduledStartTime <= endDate)
                .CountAsync();

            var completedVisits = await _context.Visits
                .Where(v => v.ScheduledStartTime >= startDate && v.ScheduledStartTime <= endDate && v.Status == "Completed")
                .CountAsync();

            var completionRate = totalVisits > 0 ? (completedVisits / (decimal)totalVisits) * 100 : 0;

            return new
            {
                TotalVisits = totalVisits,
                CompletedVisits = completedVisits,
                CompletionRate = Math.Round(completionRate, 2)
            };
        }

        public async Task<object> GetMissedVisitsAsync(DateTime startDate, DateTime endDate)
        {
            var missedVisits = await _context.Visits
                .Where(v => v.ScheduledStartTime >= startDate && v.ScheduledStartTime <= endDate && v.Status == "Cancelled")
                .Include(v => v.Patient)
                .Include(v => v.Staff)
                .ToListAsync();

            return new
            {
                Count = missedVisits.Count,
                Visits = missedVisits.Select(v => new
                {
                    v.Id,
                    PatientName = v.Patient.FullName,
                    StaffName = v.Staff.FullName,
                    ScheduledTime = v.ScheduledStartTime
                })
            };
        }

        // Financial Reports
        public async Task<object> GetRevenueReportAsync(DateTime startDate, DateTime endDate)
        {
            var revenue = await _context.Claims
                .Where(c => c.SubmittedAt >= startDate && c.SubmittedAt <= endDate && c.Status == "Paid")
                .GroupBy(c => c.SubmittedAt.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    TotalRevenue = g.Sum(c => c.AmountPaid ?? 0)
                })
                .ToListAsync();

            return revenue;
        }

        public async Task<object> GetAccountsReceivableAsync()
        {
            var outstanding = await _context.Claims
                .Where(c => c.Status == "Pending")
                .GroupBy(c => c.Visit.Patient.Id)
                .Select(g => new
                {
                    PatientId = g.Key,
                    OutstandingAmount = g.Sum(c => c.AmountBilled)
                })
                .ToListAsync();

            return outstanding;
        }

        public async Task<object> GetPayerAnalysisAsync()
        {
            var payerAnalysis = await _context.Claims
                .Join(_context.Visits, c => c.Id, v => v.Id, (c, v) => new { Claim = c, Visit = v })
                .Join(_context.Patients, x => x.Visit.PatientId, p => p.Id, (x, p) => new { x.Claim, p.Id })
                .Join(_context.Insurances, x => x.Id, i => i.PatientId, (x, i) => new { x.Claim, Insurance = i })
                .GroupBy(x => x.Insurance.ProviderName)
                .Select(g => new
                {
                    Payer = g.Key,
                    ClaimCount = g.Count(),
                    TotalAmount = g.Sum(x => x.Claim.AmountBilled)
                })
                .ToListAsync();

            return payerAnalysis;
        }

        // Clinical Reports
        public async Task<object> GetPatientOutcomesAsync(DateTime startDate, DateTime endDate)
        {
            var outcomes = await _context.PatientAssessments
                .Where(pa => pa.AssessmentDate >= startDate && pa.AssessmentDate <= endDate)
                .GroupBy(pa => pa.AssessmentType)
                .Select(g => new
                {
                    Type = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            return outcomes;
        }

        public async Task<object> GetCarePlanComplianceAsync()
        {
            var carePlans = await _context.CarePlans
                .Where(cp => cp.Status == "Active")
                .CountAsync();

            var completedInterventions = await _context.CarePlanInterventions
                .Where(cpi => cpi.Status == "Active")
                .CountAsync();

            var complianceRate = carePlans > 0 ? (completedInterventions / (decimal)carePlans) * 100 : 0;

            return new
            {
                ActiveCarePlans = carePlans,
                CompletedInterventions = completedInterventions,
                ComplianceRate = Math.Round(complianceRate, 2)
            };
        }

        public async Task<object> GetIncidentReportsAsync(DateTime startDate, DateTime endDate)
        {
            // This would integrate with incident reporting system (Phase 2)
            return new { Message = "Incident reporting available in Phase 2" };
        }

        // Compliance Reports
        public async Task<object> GetLicenseExpiryReportAsync()
        {
            var expiring = await _context.Licenses
                .Where(l => l.ExpiryDate <= DateTime.Now.AddDays(30) && l.ExpiryDate > DateTime.Now)
                .Include(l => l.Staff)
                .Select(l => new
                {
                    StaffName = l.Staff.FullName,
                    LicenseType = l.LicenseType,
                    ExpiryDate = l.ExpiryDate
                })
                .ToListAsync();

            var expired = await _context.Licenses
                .Where(l => l.ExpiryDate <= DateTime.Now)
                .Include(l => l.Staff)
                .Select(l => new
                {
                    StaffName = l.Staff.FullName,
                    LicenseType = l.LicenseType,
                    ExpiryDate = l.ExpiryDate
                })
                .ToListAsync();

            return new
            {
                ExpiringLicenses = expiring,
                ExpiredLicenses = expired
            };
        }

        public async Task<object> GetTrainingCompletionAsync()
        {
            // Training module implementation in Phase 2
            return new { Message = "Training tracking available in Phase 2" };
        }

        public async Task<object> GetQualityMetricsAsync()
        {
            var metrics = await _context.QualityAssurances.ToListAsync();
            return metrics.Select(m => new
            {
                m.MetricName,
                m.TargetValue,
                m.CurrentValue,
                m.Status,
                m.Category
            });
        }
    }
}
