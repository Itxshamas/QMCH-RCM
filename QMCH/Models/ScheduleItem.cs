using System.ComponentModel.DataAnnotations;

namespace QMCH.Models
{
    public class ScheduleItem
    {
        public int Id { get; set; }

        public int? EmployeeId { get; set; }

        [StringLength(300)]
        public string? EmployeeName { get; set; }

        public int? ClientId { get; set; }

        [StringLength(300)]
        public string? ClientName { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? EndTime { get; set; }

        public bool IsAllDay { get; set; }

        [StringLength(50)]
        public string? RecurrencePattern { get; set; }

        [StringLength(300)]
        public string? Location { get; set; }

        [StringLength(50)]
        public string? ServiceType { get; set; }

        [StringLength(50)]
        public string? ScheduleType { get; set; }

        [StringLength(30)]
        public string? Color { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Scheduled";

        public string? Notes { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
