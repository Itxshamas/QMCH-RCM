using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMCH.Models
{
    public class Staff
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string StaffId { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [StringLength(100)]
        public string? MiddleName { get; set; }

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        [Required]
        public DateTime DateOfBirth { get; set; }

        [StringLength(10)]
        public string? Gender { get; set; }

        [Required, StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Phone { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        [Required, StringLength(50)]
        public string Role { get; set; } = "Nurse"; // e.g., Nurse, LPN, CNA, Therapist, Aide

        [StringLength(50)]
        public string Status { get; set; } = "Active"; // Active, Inactive, On Leave

        public DateTime HireDate { get; set; } = DateTime.Now;

        [StringLength(50)]
        public string? LicenseNumber { get; set; }

        public DateTime? LicenseExpiryDate { get; set; }

        [StringLength(100)]
        public string? Department { get; set; }

        public decimal? HourlyRate { get; set; }

        public bool IsAvailable { get; set; } = true;

        // Emergency Contact
        [StringLength(100)]
        public string? EmergencyContactName { get; set; }

        [StringLength(20)]
        public string? EmergencyContactPhone { get; set; }

        // Navigation Properties
        public ICollection<Visit> Visits { get; set; } = new List<Visit>();
        public ICollection<StaffSkill> StaffSkills { get; set; } = new List<StaffSkill>();

        // Audit Fields
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
