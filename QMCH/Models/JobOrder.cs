using System.ComponentModel.DataAnnotations;

namespace QMCH.Models
{
    public class JobOrder
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Code { get; set; } = string.Empty;

        [Required, StringLength(200)]
        public string Title { get; set; } = string.Empty;

        public int NumberOfVisa { get; set; }
        public int FilledVisa { get; set; }

        [StringLength(50)]
        public string Priority { get; set; } = "Medium";

        [StringLength(50)]
        public string Status { get; set; } = "Approved";

        [StringLength(100)]
        public string RaisedBy { get; set; } = string.Empty;

        public DateTime DueDate { get; set; } = DateTime.Today;

        public string? Description { get; set; }
        public string? Requirements { get; set; }
        public string? JobDescription { get; set; }
        public string? RequiredQualification { get; set; }
        public string? ExperienceRange { get; set; }
        public string? EmploymentStatus { get; set; }
        public string? RequiredSkills { get; set; }
        public bool IsVisibleToDirectApplicant { get; set; }

        [StringLength(100)]
        public string? Department { get; set; }

        [StringLength(200)]
        public string? Location { get; set; }

        public int NumberOfPositions { get; set; } = 1;

        public decimal? PayRangeMin { get; set; }
        public decimal? PayRangeMax { get; set; }

        public DateTime PostedDate { get; set; } = DateTime.Today;
        public DateTime? ClosedDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
