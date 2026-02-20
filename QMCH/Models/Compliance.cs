using System.ComponentModel.DataAnnotations;

namespace QMCH.Models
{
    public class License
    {
        [Key]
        public int Id { get; set; }

        public int StaffId { get; set; }
        public Staff Staff { get; set; } = null!;

        [Required, StringLength(100)]
        public string LicenseType { get; set; } = string.Empty; // RN, LPN, CNA, etc.

        [Required, StringLength(50)]
        public string LicenseNumber { get; set; } = string.Empty;

        [Required]
        public DateTime IssuedDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [StringLength(50)]
        public string? State { get; set; }

        [StringLength(500)]
        public string? FilePath { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Active";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

    public class ComplianceRequirement
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty; // Background Check, TB Test, CPR, etc.

        [StringLength(500)]
        public string? Description { get; set; }

        public int FrequencyInMonths { get; set; } = 12;

        [StringLength(50)]
        public string Status { get; set; } = "Active";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class StaffCompliance
    {
        [Key]
        public int Id { get; set; }

        public int StaffId { get; set; }
        public Staff Staff { get; set; } = null!;

        public int ComplianceRequirementId { get; set; }
        public ComplianceRequirement ComplianceRequirement { get; set; } = null!;

        public DateTime CompletionDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Completed"; // Completed, Expired, Pending

        [StringLength(500)]
        public string? DocumentPath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

    public class QualityAssurance
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string MetricName { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public decimal TargetValue { get; set; }

        public decimal CurrentValue { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "On Track"; // On Track, At Risk, Failed

        [StringLength(50)]
        public string Category { get; set; } = "Clinical"; // Clinical, Operational, Financial

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
