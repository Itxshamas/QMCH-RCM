using System.ComponentModel.DataAnnotations;

namespace QMCH.Models
{
    public class PatientAssessment
    {
        [Key]
        public int Id { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        [Required, StringLength(100)]
        public string AssessmentType { get; set; } = string.Empty; // OASIS, Initial, Ongoing, Discharge

        [StringLength(2000)]
        public string? PhysicalExamination { get; set; }

        [StringLength(2000)]
        public string? MentalStatusExamination { get; set; }

        [StringLength(2000)]
        public string? FunctionalStatus { get; set; }

        [StringLength(2000)]
        public string? SafetyRisks { get; set; }

        [StringLength(2000)]
        public string? SpecialNeeds { get; set; }

        [StringLength(2000)]
        public string? Recommendations { get; set; }

        public string? AssessedBy { get; set; }
        public User? AssessedByUser { get; set; }

        public DateTime AssessmentDate { get; set; } = DateTime.Now;

        [StringLength(50)]
        public string Status { get; set; } = "Completed";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
