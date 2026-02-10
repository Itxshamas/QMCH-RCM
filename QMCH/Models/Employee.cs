using System.ComponentModel.DataAnnotations;

namespace QMCH.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(10)]
        public string? Gender { get; set; }

        [StringLength(300)]
        public string? Address { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(50)]
        public string? State { get; set; }

        [StringLength(20)]
        public string? ZipCode { get; set; }

        [StringLength(100)]
        public string? JobTitle { get; set; }

        [StringLength(100)]
        public string? Department { get; set; }

        public DateTime? HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }

        public decimal? PayRate { get; set; }

        [StringLength(50)]
        public string? PayType { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Active";

        [StringLength(100)]
        public string? LicenseNumber { get; set; }

        public DateTime? LicenseExpiry { get; set; }
        public DateTime? CertificationExpiry { get; set; }

        public string? Skills { get; set; }
        public string? Certifications { get; set; }
        public string? Notes { get; set; }

        public int ClassificationId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
