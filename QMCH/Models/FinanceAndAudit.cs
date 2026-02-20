using System.ComponentModel.DataAnnotations;

namespace QMCH.Models
{
    public class Insurance
    {
        [Key]
        public int Id { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        [Required, StringLength(100)]
        public string ProviderName { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string PolicyNumber { get; set; } = string.Empty;

        [StringLength(50)]
        public string? GroupNumber { get; set; }

        public DateTime? EffectiveDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public bool IsPrimary { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class Claim
    {
        [Key]
        public int Id { get; set; }

        public int VisitId { get; set; }
        public Visit Visit { get; set; } = null!;

        [Required, StringLength(50)]
        public string ClaimNumber { get; set; } = string.Empty;

        public decimal AmountBilled { get; set; }

        public decimal? AmountPaid { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // Pending, Paid, Denied

        public DateTime SubmittedAt { get; set; } = DateTime.Now;

        public DateTime? ProcessedAt { get; set; }
    }

    public class Medication
    {
        [Key]
        public int Id { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        [Required, StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Dosage { get; set; }

        [StringLength(100)]
        public string? Frequency { get; set; }

        [StringLength(100)]
        public string? Route { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class AuditLog
    {
        [Key]
        public int Id { get; set; }

        public string? UserId { get; set; }
        public User? User { get; set; }

        [Required, StringLength(100)]
        public string Action { get; set; } = string.Empty; // Create, Update, Delete, Login, etc.

        [Required, StringLength(100)]
        public string EntityName { get; set; } = string.Empty;

        public string? EntityId { get; set; }

        public string? OldValues { get; set; }

        public string? NewValues { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.Now;

        [StringLength(50)]
        public string? IPAddress { get; set; }
    }
}
