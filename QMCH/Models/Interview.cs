using System.ComponentModel.DataAnnotations;

namespace QMCH.Models
{
    public class Interview
    {
        public int Id { get; set; }

        public int? ApplicantId { get; set; }

        public int? JobOrderId { get; set; }

        [StringLength(200)]
        public string? ApplicantName { get; set; }

        [StringLength(200)]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? MobileNumber { get; set; }

        [StringLength(200)]
        public string? Position { get; set; }

        [StringLength(200)]
        public string? InterviewerName { get; set; }

        public DateTime ScheduledDateTime { get; set; } = DateTime.Now;

        public DateTime? ScheduledTime { get; set; }

        [StringLength(50)]
        public string InterviewType { get; set; } = "Phone Screen";

        [StringLength(200)]
        public string? Location { get; set; }

        [StringLength(100)]
        public string? Country { get; set; }

        [StringLength(100)]
        public string? State { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Scheduled";

        public string? Notes { get; set; }
        public string? Feedback { get; set; }

        public int? Rating { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
