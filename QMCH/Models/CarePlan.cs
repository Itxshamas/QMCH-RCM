using System.ComponentModel.DataAnnotations;

namespace QMCH.Models
{
    public class CarePlan
    {
        [Key]
        public int Id { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        [Required, StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(2000)]
        public string? Goals { get; set; }

        [StringLength(2000)]
        public string? Interventions { get; set; }

        [StringLength(2000)]
        public string? ExpectedOutcomes { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Active"; // Active, Inactive, Completed

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime? EndDate { get; set; }

        public string? CreatedBy { get; set; }
        public User? CreatedByUser { get; set; }

        public string? ModifiedBy { get; set; }
        public User? ModifiedByUser { get; set; }

        // Navigation
        public ICollection<CarePlanIntervention> Interventions_Collection { get; set; } = new List<CarePlanIntervention>();

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

    public class CarePlanIntervention
    {
        [Key]
        public int Id { get; set; }

        public int CarePlanId { get; set; }
        public CarePlan CarePlan { get; set; } = null!;

        [Required, StringLength(200)]
        public string InterventionName { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        [StringLength(50)]
        public string Frequency { get; set; } = "Daily"; // Daily, Weekly, As Needed, etc.

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime? TargetCompletionDate { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Active";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
