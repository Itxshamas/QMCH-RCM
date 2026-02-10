using System.ComponentModel.DataAnnotations;

namespace QMCH.Models
{
    public class Client
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

        [StringLength(200)]
        public string? InsuranceProvider { get; set; }

        [StringLength(100)]
        public string? InsurancePolicyNumber { get; set; }

        [StringLength(200)]
        public string? PrimaryDiagnosis { get; set; }

        [StringLength(200)]
        public string? PhysicianName { get; set; }

        [StringLength(20)]
        public string? PhysicianPhone { get; set; }

        [StringLength(200)]
        public string? EmergencyContactName { get; set; }

        [StringLength(20)]
        public string? EmergencyContactPhone { get; set; }

        [StringLength(100)]
        public string? EmergencyContactRelation { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Active";

        public string? Notes { get; set; }

        public int ClientTypeId { get; set; }
        public int ServiceTypeId { get; set; }
        public int LocationId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
