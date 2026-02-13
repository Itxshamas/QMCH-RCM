using System.ComponentModel.DataAnnotations;

namespace QMCH.Models
{
    public class Resume
    {
        public int Id { get; set; }

        public int ApplicantId { get; set; }

        // Personal Information
        [StringLength(100)]
        public string? MiddleName { get; set; }

        [StringLength(50)]
        public string? Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        // Criminal Conviction
        [StringLength(10)]
        public string? CriminalConviction { get; set; }

        public string? CriminalConvictionDetails { get; set; }

        // Challenges and Concerns
        public string? ChallengesDuringWork { get; set; }

        public string? ChallengingWorkTypes { get; set; }

        // Education
        public string? EducationJson { get; set; } // Store as JSON

        // Skills
        public string? SkillsJson { get; set; } // Store as JSON

        // Employment History
        public string? EmploymentHistoryJson { get; set; } // Store as JSON

        // Contact Information
        [StringLength(200)]
        public string? Street { get; set; }

        [StringLength(100)]
        public string? Country { get; set; }

        [StringLength(100)]
        public string? State { get; set; }

        [StringLength(20)]
        public string? PostalCode { get; set; }

        [StringLength(20)]
        public string? Contact1 { get; set; }

        [StringLength(20)]
        public string? Contact2 { get; set; }

        // Availability
        [StringLength(50)]
        public string? AvailabilityForImmediateWork { get; set; }

        [StringLength(50)]
        public string? AvailabilityForNightShift { get; set; }

        [StringLength(50)]
        public string? AvailabilityForWeekendWork { get; set; }

        [StringLength(50)]
        public string? AvailabilityForOtherLocations { get; set; }

        // Employment Preferences
        [StringLength(100)]
        public string? PreferredEmploymentType { get; set; }

        [DataType(DataType.Date)]
        public DateTime? AvailableFromDate { get; set; }

        public bool IsAvailableToStartImmediately { get; set; }

        // References
        public string? ReferencesJson { get; set; } // Store as JSON

        // Skills Matrix
        public string? SkillsMatrixJson { get; set; } // Store as JSON

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

    // Supporting Classes for Skills, Education, etc.
    public class ResumeEducation
    {
        public string? Degree { get; set; }
        public string? Institution { get; set; }
        public string? Major { get; set; }
        public string? CompletionDate { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
    }

    public class ResumeEmployment
    {
        public string? Company { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public string? JobTitle { get; set; }
        public string? Location { get; set; }
        public string? Experience { get; set; }
        public bool MayWeContact { get; set; }
        public string? Duties { get; set; }
    }

    public class ResumeSkill
    {
        public string? SkillName { get; set; }
        public string? Level { get; set; }
    }

    public class ResumeReference
    {
        public string? Name { get; set; }
        public string? Relationship { get; set; }
        public string? YearsKnown { get; set; }
        public string? Phone { get; set; }
    }

    public class ResumeSkillsMatrix
    {
        public string? SkillName { get; set; }
        public bool Proficient { get; set; }
        public bool Intermediate { get; set; }
        public bool Beginner { get; set; }
    }
}
