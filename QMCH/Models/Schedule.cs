using System.ComponentModel.DataAnnotations;

namespace QMCH.Models
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }

        public int StaffId { get; set; }
        public Staff Staff { get; set; } = null!;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime ShiftStartTime { get; set; }

        [Required]
        public DateTime ShiftEndTime { get; set; }

        public bool IsAvailable { get; set; } = true;

        [StringLength(500)]
        public string? Notes { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Active"; // Active, On Leave, Off

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
