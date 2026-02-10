using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMCH.Models
{
    public class TimesheetPayroll
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        [StringLength(200)]
        public string? EmployeeName { get; set; }

        public DateTime PeriodStart { get; set; }
        public DateTime PeriodEnd { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal RegularHours { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OvertimeHours { get; set; }

        [NotMapped]
        public decimal TotalHours => RegularHours + OvertimeHours;

        [Column(TypeName = "decimal(18,2)")]
        public decimal PayRate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OvertimeRate { get; set; }

        [NotMapped]
        public decimal TotalAmount => (RegularHours * PayRate) + (OvertimeHours * OvertimeRate);

        [StringLength(50)]
        public string Status { get; set; } = "Pending";

        public string? Notes { get; set; }

        public string? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
