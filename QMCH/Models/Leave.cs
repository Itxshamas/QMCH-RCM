using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMCH.Models
{
    public class Leave
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        [StringLength(200)]
        public string EmployeeName { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string LeaveType { get; set; } = "Annual";

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [NotMapped]
        public int TotalDays => (EndDate - StartDate).Days + 1;

        public string? Reason { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Pending";

        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string? RejectionReason { get; set; }

        public DateTime RequestedDate { get; set; } = DateTime.Today;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
