using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMCH.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string PatientId { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(100)]
        public string? MiddleName { get; set; }

        [Required, StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        [StringLength(20)]
        public string? SSN { get; set; } // Should be encrypted

        [Required]
        public DateTime DateOfBirth { get; set; }

        [StringLength(10)]
        public string? Gender { get; set; }

        [StringLength(200)]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Active";

        [StringLength(500)]
        public string? PrimaryDiagnosis { get; set; }

        public DateTime AdmissionDate { get; set; } = DateTime.Now;

        // Emergency Contact
        [StringLength(100)]
        public string? EmergencyContactName { get; set; }

        [StringLength(20)]
        public string? EmergencyContactPhone { get; set; }

        // Navigation Properties
        public ICollection<Visit> Visits { get; set; } = new List<Visit>();
        public ICollection<Insurance> Insurances { get; set; } = new List<Insurance>();
        public ICollection<Medication> Medications { get; set; } = new List<Medication>();

        // Audit Fields
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
