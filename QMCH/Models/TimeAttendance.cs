using System.ComponentModel.DataAnnotations;

namespace QMCH.Models
{
    public class TimeAttendance
    {
        [Key]
        public int Id { get; set; }

        public int StaffId { get; set; }
        public Staff Staff { get; set; } = null!;

        [Required]
        public DateTime ClockInTime { get; set; }

        public DateTime? ClockOutTime { get; set; }

        [StringLength(100)]
        public string? ClockInLocation { get; set; }

        [StringLength(100)]
        public string? ClockOutLocation { get; set; }

        [StringLength(500)]
        public string? GPSCoordinates { get; set; }

        public decimal? HoursWorked { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Active"; // Active, Completed, Cancelled

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

    public class TimeOffRequest
    {
        [Key]
        public int Id { get; set; }

        public int StaffId { get; set; }
        public Staff Staff { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required, StringLength(50)]
        public string Type { get; set; } = "PTO"; // PTO, Sick, Holiday, Unpaid

        [StringLength(500)]
        public string? Reason { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // Pending, Approved, Denied

        public string? ApprovedBy { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
