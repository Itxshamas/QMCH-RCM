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

        [StringLength(100)]
        public string? MiddleName { get; set; }

        // Personal Data
        [StringLength(20)]
        public string? Phone1 { get; set; }

        [StringLength(20)]
        public string? Phone2 { get; set; }

        // Backward compatibility
        [StringLength(20)]
        public string? Phone
        {
            get => Phone1;
            set => Phone1 = value;
        }

        [StringLength(200)]
        public string? Email { get; set; }

        [StringLength(300)]
        public string? Street { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(50)]
        public string? State { get; set; }

        [StringLength(100)]
        public string? Country { get; set; }

        [StringLength(20)]
        public string? ZipCode { get; set; }

        // Contract Details
        [StringLength(50)]
        public string? QatarIdNumber { get; set; }

        public DateTime? QatarIdIssued { get; set; }

        public DateTime? QatarIdExpire { get; set; }

        [StringLength(50)]
        public string? QCHPLicenseNumber { get; set; }

        public DateTime? QCHPLicenseIssued { get; set; }

        public DateTime? QCHPLicenseExpire { get; set; }

        [StringLength(50)]
        public string? PassportNumber { get; set; }

        public DateTime? PassportIssued { get; set; }

        public DateTime? PassportExpire { get; set; }

        [StringLength(50)]
        public string? PRCRegistrationNumber { get; set; }

        public DateTime? PRCIdentificationIssued { get; set; }

        public DateTime? PRCIdentificationExpire { get; set; }

        public DateTime? PreliminaryAssessmentIssued { get; set; }

        public DateTime? PreliminaryAssessmentExpire { get; set; }

        // Legal Documents / Employment Details
        public DateTime? JoinDate { get; set; }

        [StringLength(100)]
        public string? ContractFor { get; set; }

        public DateTime? LastWorkingDate { get; set; }

        public DateTime? TravellingDate { get; set; }

        public bool IsEmployeeReJoined { get; set; }

        public DateTime? RejoinedDate { get; set; }

        [StringLength(100)]
        public string? BusinessUnit { get; set; }

        [StringLength(100)]
        public string? Department { get; set; }

        [StringLength(100)]
        public string? JobTitle { get; set; }

        [StringLength(100)]
        public string? Position { get; set; }

        [StringLength(50)]
        public string? Category { get; set; }

        [StringLength(100)]
        public string? RoleInSystem { get; set; }

        [StringLength(100)]
        public string? RecruitmentAgency { get; set; }

        [StringLength(50)]
        public string? ContractStatus { get; set; } = "Active";

        [StringLength(100)]
        public string? ReasonInActive { get; set; }

        [StringLength(50)]
        public string? MedicalStatus { get; set; }

        [StringLength(50)]
        public string? BloodGroupStatus { get; set; }

        [StringLength(50)]
        public string? FingerPrintStatus { get; set; }

        [StringLength(50)]
        public string? QatarIdStatus { get; set; }

        [StringLength(50)]
        public string? PoliceClearStatus { get; set; }

        [StringLength(50)]
        public string? SCHLicenseStatus { get; set; }

        // Legacy fields
        public DateTime? DateOfBirth { get; set; }

        [StringLength(10)]
        public string? Gender { get; set; }

        [StringLength(300)]
        public string? Address { get; set; }

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

        [StringLength(50)]
        public string? BloodGroup { get; set; }

        public bool IsCandidate { get; set; }

        public bool BackgroundCheckCompleted { get; set; }

        public bool DrivingLicenseVerified { get; set; }

        public bool NFCRegistered { get; set; }

        public bool OQHPLicenseVerified { get; set; }

        public bool BioMetricRegistered { get; set; }

        public byte[]? PhotoData { get; set; }

        [StringLength(20)]
        public string? PhotoMimeType { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; } = string.Empty;
    }
}
