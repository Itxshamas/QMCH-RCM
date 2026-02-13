using System.ComponentModel.DataAnnotations;

namespace QMCH.Models
{
    public class Applicant
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

        [Required, StringLength(200)]
        public string PositionApplied { get; set; } = string.Empty;

        [StringLength(50)]
        public string? Source { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "New";

        [StringLength(500)]
        public string? ResumeUrl { get; set; }

        [StringLength(500)]
        public string? PhotoUrl { get; set; }

        public string? Notes { get; set; }

        public DateTime AppliedDate { get; set; } = DateTime.Today;

        public int JobOrderId { get; set; }
        public int AgencyId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
