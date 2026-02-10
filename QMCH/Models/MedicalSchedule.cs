using System.ComponentModel.DataAnnotations;

namespace QMCH.Models
{
    public class MedicalSchedule
    {
        public int Id { get; set; }

        public int? EmployeeId { get; set; }

        [StringLength(300)]
        public string? EmployeeName { get; set; }

        [Required]
        [StringLength(300)]
        public string ExamType { get; set; } = string.Empty;

        [StringLength(300)]
        public string? Provider { get; set; }

        [StringLength(300)]
        public string? Location { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ScheduledDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CompletedDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ExpirationDate { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Scheduled";

        [StringLength(50)]
        public string? Result { get; set; }

        [StringLength(500)]
        public string? DocumentUrl { get; set; }

        public string? Notes { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
