using System.ComponentModel.DataAnnotations;

namespace QMCH.Models
{
    public class Attendance
    {
        public int Id { get; set; }

        public int? EmployeeId { get; set; }

        [StringLength(300)]
        public string? EmployeeName { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public TimeSpan? ClockIn { get; set; }

        public TimeSpan? ClockOut { get; set; }

        [StringLength(50)]
        public string? Status { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        [StringLength(500)]
        public string? GeoLocation { get; set; }

        [StringLength(500)]
        public string? IPAddress { get; set; }

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
