using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMCH.Models
{
    public class TimesheetBilling
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        [Required, StringLength(200)]
        public string ClientName { get; set; } = string.Empty;

        public DateTime PeriodStart { get; set; } = DateTime.Today.AddDays(-7);
        public DateTime PeriodEnd { get; set; } = DateTime.Today;

        public decimal TotalHours { get; set; }
        public decimal BillRate { get; set; }

        // Aligned with BillingCreateEdit.razor
        [NotMapped]
        public decimal BillingRate
        {
            get => BillRate;
            set => BillRate = value;
        }

        [StringLength(100)]
        public string? ServiceType { get; set; }

        [NotMapped]
        public decimal TotalAmount => TotalHours * BillRate;

        [StringLength(50)]
        public string Status { get; set; } = "Pending";

        [StringLength(50)]
        public string? InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
    }
}
