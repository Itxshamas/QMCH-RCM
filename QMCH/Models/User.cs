using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace QMCH.Models
{
    public class User : IdentityUser
    {
        [Required, StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";

        [StringLength(100)]
        public string? Department { get; set; }

        [StringLength(100)]
        public string? JobTitle { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? LastLoginDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation Properties
        public ICollection<Message> SentMessages { get; set; } = new List<Message>();
        public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        public ICollection<DocumentRecord> DocumentRecords { get; set; } = new List<DocumentRecord>();
        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
    }
}

