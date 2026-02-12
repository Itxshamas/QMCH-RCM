using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QMCH.Models
{
    public class HRAgency
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Agency Name is required"), StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Address { get; set; }

        [StringLength(100)]
        public string? Country { get; set; }

        [StringLength(100)]
        public string? State { get; set; }

        [StringLength(100)]
        public string? City { get; set; }

        [StringLength(20)]
        public string? PrimaryPhone { get; set; }

        [StringLength(20)]
        public string? SecondaryPhone { get; set; }

        [StringLength(20)]
        public string? Fax { get; set; }

        [StringLength(200)]
        public string? Email { get; set; }

        [StringLength(200)]
        public string? WebsiteURL { get; set; }

        [StringLength(100)]
        public string? ContactPerson { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Active";

        [NotMapped]
        public bool IsActive => Status == "Active";

        public bool IsWebloginEnabled { get; set; } = false;

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
    }
}
