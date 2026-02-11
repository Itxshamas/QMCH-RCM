using System.ComponentModel.DataAnnotations;

namespace QMCH.Models
{
    public class Client
    {
        public int Id { get; set; }

        // Basic Client Information
        [StringLength(50)]
        public string? ClientId { get; set; }

        [StringLength(50)]
        public string? QatarId { get; set; }

        [Required, StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(100)]
        public string? MiddleName { get; set; }

        [Required, StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        // Contact Information
        [StringLength(200)]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        [StringLength(20)]
        public string? Phone2 { get; set; }

        // Personal Information
        public DateTime? DateOfBirth { get; set; }

        [StringLength(10)]
        public string? Gender { get; set; }

        // Service Information
        public int ClientTypeId { get; set; }

        public int ServiceTypeId { get; set; }

        // Convenience property to expose a human-readable client type
        // Maps the ClientTypeId to a string. Adjust mapping as needed to match
        // your database or lookup values.
        public string? ClientType
        {
            get
            {
                return ClientTypeId switch
                {
                    1 => "Home",
                    2 => "Business",
                    3 => "Corporate",
                    _ => "Home",
                };
            }
        }

        [StringLength(50)]
        public string Status { get; set; } = "Active";

        [StringLength(200)]
        public string? Reason { get; set; }

        public DateTime? ServiceStartDate { get; set; }

        public DateTime? ServiceEndDate { get; set; }

        public DateTime? InquiryDate { get; set; }

        public DateTime? AssessmentDate { get; set; }

        public bool IsBlacklisted { get; set; }

        // Home Address
        [StringLength(300)]
        public string? Street { get; set; }

        [StringLength(100)]
        public string? Area { get; set; }

        [StringLength(50)]
        public string? State { get; set; }

        [StringLength(50)]
        public string? BuildingNumber { get; set; }

        [StringLength(50)]
        public string? StreetNumber { get; set; }

        [StringLength(100)]
        public string? Country { get; set; }

        [StringLength(20)]
        public string? ZipCode { get; set; }

        // Billing Address
        [StringLength(300)]
        public string? BillingStreet { get; set; }

        [StringLength(100)]
        public string? BillingArea { get; set; }

        [StringLength(50)]
        public string? BillingState { get; set; }

        [StringLength(50)]
        public string? BillingBuildingNumber { get; set; }

        [StringLength(50)]
        public string? BillingStreetNumber { get; set; }

        [StringLength(100)]
        public string? BillingCountry { get; set; }

        [StringLength(20)]
        public string? BillingZipCode { get; set; }

        // Legacy fields (kept for backwards compatibility)
        [StringLength(300)]
        public string? Address { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        // Insurance Information
        [StringLength(200)]
        public string? InsuranceProvider { get; set; }

        [StringLength(100)]
        public string? InsurancePolicyNumber { get; set; }

        // Medical Information
        [StringLength(200)]
        public string? PrimaryDiagnosis { get; set; }

        [StringLength(200)]
        public string? PhysicianName { get; set; }

        [StringLength(20)]
        public string? PhysicianPhone { get; set; }

        // Emergency Contact
        [StringLength(200)]
        public string? EmergencyContactName { get; set; }

        [StringLength(20)]
        public string? EmergencyContactPhone { get; set; }

        [StringLength(100)]
        public string? EmergencyContactRelation { get; set; }

        // Additional Information
        public string? Notes { get; set; }

        public int LocationId { get; set; }

        // Profile Picture
        [StringLength(500)]
        public string? ProfilePicturePath { get; set; }

        // GEO Location
        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        // Audit Fields
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; } = string.Empty;

        [StringLength(100)]
        public string? UpdatedBy { get; set; }
    }
}